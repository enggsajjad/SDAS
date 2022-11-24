#include <reg51.h>
#include "sdas.h"
/*
	Modification:
		04-01-2011:			Ensuring @@Hb as First Command, and @@Eq as 2nd Command
		24-01-2011:			GMT Offset Added
		09-12-2011:			EEPROM Write machenism changed
		15-05-2012:			GCF Compatible Time and Date Format
*/
void main(void)
{
   unsigned int dly;
	// No delay required initially
   // Set the 8051 Registers
   SCON = 0x52;
	TMOD = 0x21;
	TH1  = -6;
	TR1  = 1;
	// Set the pins and variables
	strobe = 1;
	bidirec_enable = 1;
	track = 0;
	start = 0;
	BKLT   = 1;
	state = idle_state;
	RxState = grabage;
	// Initializing the LCD
	InitDisp(); 
   LCDClear();	
   LCDCursor(2,4);
	DispStr("Plz wait..");

  // Get the saved site id from the EEPROM
   b1 = ROM_Read(0x0000);
   b2 = ROM_Read(0x0001);
   site_id = (b2<<8) + b1;
 	// Delay required for GPS Reciever 1 Second
 	// Increased to 2 sec; because of initialization in cold start
  	for(dly=0;dly<2000;dly++)
	 DelayOnems();
 
   SendStr(Short_Position);
   while(Getc()!=10);
	Putc('.');
	SendStr(ASCII_Position);   
	while(Getc()!=10);
	Putc('.');
	SendStr(Combined_Position);   
	while(Getc()!=10);
	Putc('.');
	
	ES = 1;
	EA = 1;
	// Ready to Recievde data from GPS
   RxState = get_first_hdr;
   
	while(1)
	{
	   if(rxFlag)
  		{
   		rxFlag = 0;
	     	switch (RxState)
			{
				case grabage:
					break;
				case get_first_hdr:
					if(Temp == '@')
						RxState = get_second_hdr;
	         	break;
				case get_second_hdr:
					if(Temp == '@')
					{
						RxState = check_first_chr;
						RxCntr = 0;
					}
					else
						RxState = get_first_hdr;
					break;
				case check_first_chr:
					if(Temp=='E')
						RxState   = check_second_chrq;	
					else if(Temp=='H')
						RxState   = check_second_chrb;	
					else
						RxState = get_first_hdr;
					break;
				case check_second_chrq:
					if(Temp=='q')
						RxState   = get_time_info;
					else
						RxState = get_first_hdr;
					break;
				case check_second_chrb:
					if(Temp=='b')
						RxState   = get_track_info;
					else
						RxState = get_first_hdr;
					break;
				case get_time_info:
					if(RxCntr<12 && Temp!=',')
						gps_time[RxCntr++] = Temp;
					else if(RxCntr==12) 
					{
				 		commands = 2;
				 		RxState = get_first_hdr;
				 		if (commands==2) {state = time_to_fpga;commands = 0;}
					}
					break;
				case get_track_info:
      	   	RxCntr++;
					if(RxCntr == 36)
         			vis_satelites = Temp;
	  				if(RxCntr == 37)
  					{
      	   		track_satelites = Temp;
						commands = 1;
						if (commands==2) state = time_to_fpga;
						RxState = get_first_hdr;

	         	}
					break;
  				}// End Switch
			}//if rxFlag
			switch(state)
			{
			 	case idle_state:
			 		break;
			 	case time_to_fpga:
			 		//commands = 0;
			 		//Convert into Seconds
			   	Convert2GCF();
					//Send Time four Bytes to FPGA
					start = 1;
		   	  	FPGA_DATA =(unsigned char) lSeconds & 0x000000FF;
		      	strobe = 0;
			      strobe = 1;
			      FPGA_DATA = (unsigned char) (lSeconds >> 8)  & 0x000000FF;
			      strobe = 0;
		   	   strobe = 1;
		      	FPGA_DATA = (unsigned char) (lSeconds >> 16) & 0x000000FF;
			      strobe = 0;
   	         strobe = 1;
				  	FPGA_DATA = (unsigned char) (lSeconds >> 24) & 0x000000FF;
		   	   strobe = 0;
		      	strobe = 1;
		      	// Send 6 Bytes More

		      	FPGA_DATA = GpsYear;
		      	GpsYear = (gps_time[4]-48)*10 + (gps_time[5]-48);      	// years
		      	strobe = 0;
		      	strobe = 1;
      			
      			FPGA_DATA = GpsMonth;
      			GpsMonth = ((gps_time[0]-48)*10+(gps_time[1]-48));     		// months
      			strobe = 0;
		      	strobe = 1;
	   			
	   			FPGA_DATA = GpsDay;
	   			GpsDay = ((gps_time[2]-48)*10+gps_time[3]-48); 				// days
	   			strobe = 0;
		      	strobe = 1;
			      
			      FPGA_DATA = GpsHour;
			      GpsHour = (gps_time[6]-48)*10+(gps_time[7]-48);        	// hours
			      strobe = 0;
			      strobe = 1;
			      
			      FPGA_DATA = GpsMinute;
			      GpsMinute = ((gps_time[8]-48)*10 + (gps_time[9]-48));       // minutes
			      strobe = 0;
		      	strobe = 1;
				   
				   FPGA_DATA = GpsSecond;
				   GpsSecond =  (gps_time[10]-48)*10+gps_time[11]-48;    		// seconds
				   strobe = 0;
		      	strobe = 1;
		      	// *****
		      	state = check_site_id;
			 		break;
			 	case check_site_id:
			 		if(flag_update_intr==0)
			 			state = get_site_id;
			 		else
			 			state = send_site_id;
			 		break;
			 	case get_site_id:
			 		bidirec_enable = 0;
		        	strobe = 0;
		      	strobe = 1;
		      	b1 = FPGA_DATA;
		  	      strobe = 0;
		      	strobe = 1;
		  	     	b2 = FPGA_DATA;
		      	site_id = (b2<<8) + b1;
		      	bidirec_enable = 1;
		      	start = 0;
		      	state = update_eeprom;
			 		break;
			 	case send_site_id:
			 		FPGA_DATA = b1;
			      strobe = 0;
		      	strobe = 1;
			     	FPGA_DATA = b2;
			      strobe = 0;
		      	strobe = 1;
		      	start = 0;
		      	state = check_track_info;
			 		break;
			 	case update_eeprom:						//09-12-2011 Updates
			 		state = check_track_info;
		 			ROM_Write(0x0000,b1);
		      	ROM_Write(0x0001,b2);
			 		break;
			 	case check_track_info:
			 		if(track_satelites && vis_satelites)
					{
					 	trackcount++;
						if(trackcount==16)
						 	track = 1;
					}
					else
					{
						track = 0;
					 	trackcount = 0;
		    		}
		    		state = display_info;
			 		break;
		 		case display_info:
					TimeWindow();
					state = idle_state;
			 		break;
		}
	}//while
}//main
void serial_isr(void) interrupt 4
{	
	if(RI)
	{
 	 	rxFlag = 1;
	 	RI = 0;
		Temp = SBUF;
	}
}












