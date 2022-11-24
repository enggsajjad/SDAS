#include <reg51.h>
#include "sdas.h"

void main(void)
{
   SCON = 0x52;
	TMOD = 0x21;
	TH1  = -6;
	TR1  = 1;
	IE = 0x90;
	strobe = 1;
	bidirec_enable = 1;
	track = 0;
	start = 0;
	BKLT   = 1;
	state = idle_state;
	RxState = get_first_hdr;
	InitDisp(); 
   LCDClear();	
   
	DelayOnems();
	DelayOnems();
   
   b1 = ROM_Read(0x0000);
   b2 = ROM_Read(0x0001);
   site_id = (b2<<8) + b1;
   
   SendStr(Short_Position);	
	SendStr(ASCII_Position);   


	while(1)
	{
	   if(rxFlag)
  		{
   		rxFlag = 0;
	     	switch (RxState)
			{
				case get_first_hdr:
					if(Temp=='q')
						RxState   = get_time_info;
					RxCntr = 0;
					break;
				case get_time_info:
					if(RxCntr<12 && Temp!=',')
						gps_time[RxCntr++] = Temp;
					else if(RxCntr==12) 
				 		RxState = check_second_chrb;
					break;
				case check_second_chrb:
					if(Temp=='b')
						RxState   = get_track_info;
					RxCntr = 0;
					break;	
				case get_track_info:
      	   	RxCntr++;
					if(RxCntr == 36)
         			vis_satelites = Temp;
	  				else if(RxCntr == 37)
  					{
      	   		track_satelites = Temp;
						state = time_to_fpga;
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
			 		//Convert into Seconds
			   	Convert2Sec();
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
		      	ROM_Write(0x0000,b1);
		      	ROM_Write(0x0001,b2);
		      	site_id = (b2<<8) + b1;
		      	bidirec_enable = 1;
		      	start = 0;
		      	state = 6;
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












