#define LCD P0
#define FPGA_DATA P1
#define Putc WriteData

#define get_first_hdr		1
#define get_second_hdr		2
#define check_first_chr		3
#define check_second_chrq	4
#define check_second_chrb	5
#define get_time_info		6
#define get_track_info		7
#define idle_state			8
#define time_to_fpga			9
#define check_site_id		10
#define get_site_id			11
#define send_site_id			12
#define check_track_info	13
#define display_info			14
#define grabage				15
#define update_eeprom		16

sbit RS			=	P2^7;
sbit RW			=	P2^6;
sbit ELCD		=	P2^5;
sbit BKLT		=	P2^4;
sbit BS  		= P0^7;
sbit strobe		=	P3^7;
sbit track		=	P3^6;	
sbit 	eSDA 		= 	P2^0;
sbit 	eSCL 		= 	P2^1;
sbit 	eRW		=  P2^2;
sbit 	flag_update_intr		=  P2^3;
sbit 	bidirec_enable		=  P3^5;
sbit 	start		=  P3^4;

idata unsigned char GpsYear,GpsMonth,GpsDay,GpsHour,GpsMinute,GpsSecond;
unsigned char b1,b2;  
unsigned int site_id,site_id_q,site_id_r;
unsigned char sdata;
idata unsigned char gps_time[13];
unsigned char track_satelites;
unsigned char vis_satelites;
unsigned long lSeconds;
unsigned char Day;
unsigned char trackcount;
unsigned char Temp;
unsigned char rxFlag;
unsigned char RxState;
unsigned char state;
unsigned char RxCntr;
unsigned char commands=0;

unsigned char Templ;
// 40 40 41	62 00 05 00
idata char GMT_Offset[]=  {'@', '@', 'A', 'b', 0x00, 0x05, 0x00,0x26, 0x0D, 0x0A,0};
// 40 40 48	62 01
idata char Short_Position[]=  {'@', '@', 'H', 'b', 0x01, 0x2B, 0x0D, 0x0A,0};
// 40 40 45 71 01
idata char ASCII_Position[]= {'@', '@', 'E', 'q', 0x01, 0x35, 0x0D, 0x0A,0};
idata char Combined_Position[] = {'@','@','G','a',0x07,0x39,0xC0,0xEC,0x0F,0xAD,0x3F,0x28,0x00,0x00,0xBF,0x1C,0x00,0x22,0x0D,0x0A,0};
unsigned long LUT[]={0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334};

void DelayOnems(void);

void LcdBusy()
{
		BS   = 1;		//Make D7th bit of LCD as i/p
   	ELCD   = 1;       //Make port pin as o/p
   	RS   = 0;       //Selected command register
   	RW   = 1;		//We are reading
   	while(BS)
   	{				//read busy flag again and again till it becomes 0 Enable H->L
   		ELCD   = 0;
      	ELCD   = 1;
   	}
}
void WriteControl(unsigned char var)
{
		P0 = var;		//Commands to be Written
   	RS   = 0;       //Selected command register
   	RW   = 0;       //We are writing in instruction register
   	ELCD   = 1;       //Enable H->L
   	ELCD   = 0;
   	LcdBusy();		//Wait for LCD to process the command
}
void WriteData(unsigned char var)
{
		P0 = var;		//Data/Character to be Written
   	RS   = 1;       //Selected data register
   	RW   = 0;       //We are writing
   	ELCD   = 1;       //Enable H->L
   	ELCD   = 0;
   	LcdBusy();      //Wait for LCD to process the command
}

void InitDisp()
{
	WriteControl(0x38);
	DelayOnems();
	WriteControl(0x38);
   DelayOnems();
   WriteControl(0x38);
   DelayOnems();
	WriteControl(0x06);
	DelayOnems();
	WriteControl(0x0c);
	DelayOnems();
}
void DispStr(char *s) 
{
	
	while (*s)
		WriteData(*s++);
}
void LCDCursor(unsigned char row, unsigned char col)
{
	switch (row)
	{
		case 1: WriteControl(0x80 + col - 1); break;
		case 2: WriteControl(0xc0 + col - 1); break;
		case 3: WriteControl(0x94 + col - 1); break;
		case 4: WriteControl(0xd4 + col - 1); break;
		default: break;
	}
	DelayOnems();
	DelayOnems();
}
void DelayOnems(void)
{
	TL0 = 0xCD;
	TH0 = 0xF8;
	TR0 = 1;
	while(!TF0);
	TR0 = 0;
	TF0 = 0;
}
void LCDClear()
{
 	WriteControl(0x01);
 	DelayOnems();
 	DelayOnems();
}
unsigned char Getc(void)
{
	unsigned char c;
	while(!RI);	c = SBUF; RI = 0;
	return(c);
}
void SendChar(unsigned char ch)
{
	TI=1;
	while (!TI);	TI=0;	SBUF = ch;
	while (!TI);	TI=0; 	
}
void SendStr(char *cmd)
{
 	TI=1;
 	while(*cmd)
 	{
 		while (!TI);	TI=0;	SBUF = *cmd;
 	 	cmd++;
 	}
 	while (!TI);	TI=0;
}
void Convert2GCF(void)
{
     	unsigned char L = 0;
     	// Reference Date : 17-11-1989	00:00:00
     	Templ    = (gps_time[4]-48)*10 + (gps_time[5]-48);      				// years
     	//check that the Current Year is a leap year
     	if ((Templ%4) == 0 ) L = 1; 
     	// subtract the 1989 from current year
     	Templ 	= Templ + 11; //i.e. 2000-1989 = 11
     	// calculate the # of days since 1-1-1989 with leap adjustments
   	lSeconds = Templ * 365 + Templ/4 + L;
      Day      = ((gps_time[0]-48)*10+(gps_time[1]-48))-1;     			// months
	   lSeconds = lSeconds + LUT[Day];
	   // calculate the # of days since 71-11-1989
	   lSeconds = lSeconds + ((gps_time[2]-48)*10+gps_time[3]-48) - 321; // days
	   lSeconds = lSeconds << 17;

      Templ    = (gps_time[6]-48)*10+(gps_time[7]-48);           			// hours
	   lSeconds = lSeconds + Templ*1800 + Templ*1800;
      Templ    = ((gps_time[8]-48)*10 + (gps_time[9]-48));       			// minutes
	   lSeconds = lSeconds + Templ*60;
	   lSeconds = lSeconds + (gps_time[10]-48)*10+gps_time[11]-48;    	// seconds
}
/*
void Convert2Sec(void)
{
     	Templ    = (gps_time[4]-48)*10 + (gps_time[5]-48);      // years
   	lSeconds = Templ * 365 + Templ/4;
      Day      = ((gps_time[0]-48)*10+(gps_time[1]-48))-1;     // months
	   lSeconds = lSeconds + LUT[Day];
	   lSeconds = lSeconds + ((gps_time[2]-48)*10+gps_time[3]-48); // days
	   //0x15180 = 86400 = 24*3600
	   //0x386D4380 = ( 946684800 = 30*365 + 7 ) * 86400
   	lSeconds = lSeconds * 0x15180 + 0x386D4380;
      Templ    = (gps_time[6]-48)*10+(gps_time[7]-48);           // hours
	   lSeconds = lSeconds + Templ*1800 + Templ*1800;
      Templ    = ((gps_time[8]-48)*10 + (gps_time[9]-48));       // minutes
	   lSeconds = lSeconds + Templ*60;
	   lSeconds = lSeconds + (gps_time[10]-48)*10+gps_time[11]-48;    // seconds
} */
void TimeWindow(void)
{
      	LCDCursor(1, 1);
         DispStr("   Site ID: ");
         site_id_q = site_id/10000;
         Putc(site_id_q+48);
         site_id_r = site_id - site_id_q*10000;
         site_id_q = site_id_r/1000;
         Putc(site_id_q+48);
         site_id_r = site_id_r - site_id_q*1000;
         site_id_q = site_id_r/100;
         Putc(site_id_q+48);
         site_id_r = site_id_r - site_id_q*100;
         site_id_q = site_id_r/10;
         Putc(site_id_q+48);
         site_id_r = site_id_r - site_id_q*10;
         Putc(site_id_r+48);

         LCDCursor(2, 3);
         DispStr("VISI:");
         Putc((vis_satelites/10)+48);
         Putc((vis_satelites%10)+48);
         
         DispStr("  TRAC:");
         Putc((track_satelites/10)+48);Putc((track_satelites%10)+48);
         LCDCursor(3, 2);
         DispStr(" DATE: ");
         Putc(gps_time[2]);  Putc(gps_time[3]);Putc('-');
         Putc(gps_time[0]);
         Putc(gps_time[1]);Putc('-');
         Putc('2'); Putc('0');
         Putc(gps_time[4]); Putc(gps_time[5]);
         LCDCursor(4, 4);
         DispStr("TIME: ");
         Putc(gps_time[6]); Putc(gps_time[7]);Putc(':');
         Putc(gps_time[8]); Putc(gps_time[9]);Putc(':');
         Putc(gps_time[10]); Putc(gps_time[11]);

        
}
// EEPROM

//====== 		E E P R O M 		F U N C T I O N 		D E F I N I T I O N S			=======

void I2C_delay(void)
{
	unsigned char k;
	for(k=0; k<15; k++);
}
void I2C_clock(void)
{
	I2C_delay();
	eSCL = 1;		// Start clock  
	I2C_delay();    
	eSCL = 0;		// Clear eSCL  
}
void I2C_start(void)
{
	if(eSCL)
	eSCL = 0;		// Clear eSCL  
	eSDA = 1;        // Set eSDA  
	eSCL = 1;		// Set eSCL  
	I2C_delay(); 
	eSDA = 0;        // Clear eSDA  
	I2C_delay(); 
	eSCL = 0;        // Clear eSCL  
}
 
void I2C_stop(void)
{
	if(eSCL)	
	eSCL = 0;			// Clear eSCL  
	eSDA = 0;			// Clear eSDA  
	I2C_delay();
	eSCL = 1;			// Set eSCL  
	I2C_delay();
	eSDA = 1;			// Set eSDA  
}
 
bit I2C_write(unsigned char dat)
{
	bit data_bit;		
	unsigned char i;	
	for(i=0;i<8;i++)				// For loop 8 time(send data 1 byte)  
	{
		data_bit = dat & 0x80;		// Filter MSB bit keep to data_bit  
		eSDA = data_bit;				// Send data_bit to eSDA  
		I2C_clock();      			// Call for send data to i2c bus  
		dat = dat<<1;  
	}
	eSDA = 1;			// Set eSDA  
	I2C_delay();	
	eSCL = 1;			// Set eSCL  
	I2C_delay();	
	data_bit = eSDA;   	// Check acknowledge  
	eSCL = 0;			// Clear eSCL  
	I2C_delay();
	return data_bit;	// If send_bit = 0 i2c is valid  		 	
}
 
unsigned char I2C_read(void)
{
	bit rd_bit;	
	unsigned char i, dat;
	dat = 0x00;	
	for(i=0;i<8;i++)		// For loop read data 1 byte  
	{
		I2C_delay();
		eSCL = 1;			// Set eSCL  
		I2C_delay(); 
		rd_bit = eSDA;		// Keep for check acknowledge	 
		dat = dat<<1;		
		dat = dat + rd_bit;	// Keep bit data in dat  
		eSCL = 0;			// Clear eSCL  
	}
	return dat;
}
void I2C_noack()
{
	eSDA = 1;		// Set eSDA  
	I2C_delay();
	I2C_clock();	// Call for send data to i2c bus  
	eSCL = 1;		// Set eSCL  
}
unsigned char ROM_Read(unsigned int addr)
{
	unsigned char dat;
	eRW = 1;	
	I2C_start();            // Start i2c bus  
	I2C_write(0xA0);   // Connect to EEPROM  
	I2C_write((addr&0xFF00)/0xFF);	 // Request RAM address (Hight byte)  
	I2C_write(addr&0x00FF);	 // Request RAM address (Low byte)  
	I2C_start();			// Start i2c bus  
	I2C_write(0xA1);// Connect to EEPROM for Read  
	eRW = 0;
	dat = I2C_read();		// Receive data 
	eRW = 1; 
	I2C_noack();
	I2C_stop();				// Stop i2c bus  
   return dat;			
}
 
void ROM_Write(unsigned int addr, unsigned char val)
{
	eRW = 1;
	I2C_start(); 
	I2C_write(0xA0);   						// Connect to EEPROM  
	I2C_write((addr&0xFF00)/0xFF);	 	// Request RAM address (Hight byte)  
	I2C_write(addr&0x00FF);	 				// Request RAM address (Low byte) 
	I2C_write(val);						// Write sec on RAM specified address
	I2C_stop();           					// Stop i2c bus
	DelayOnems();
	DelayOnems();
	DelayOnems();
	DelayOnems();
	DelayOnems();
}

