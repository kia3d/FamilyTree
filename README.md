# FamilyTree

This is a dotnet core 3.1 project should be runable cross platform 
For the sake of simplicity, I assumed name are unique.
the other assumtion is Queen and King already hardcoded so data file must not have them.
I did not creat a multitier project but recpected the single responcibiliy rules

I also added AddPartner so we can add Hosband/Wife too

To run in windows
just execute the executable with the fullpath to the filename 
e.g.
FamilyTree.exe c:\temp\datafile.txt

I also added some unit test but only to FamilyTree (we should have one for CommandRunner too) class for important paths (not a %100 coverage but just to give the reviewer an idea). 
I did not used any particular DI (so I dont have to add DI lib) but in the Program class (starter of console app) the dependacy are injected in constructor manually

Main parts of the system
1- FamilyTree class
	The class that define the FamilyTree and actions
2- CommandRunner
	The class that read text and executes commands in it
3- Program class
	This is the console app entry which read the file and passes data to CommandRunner class 


here is an example file

ADD_CHILD Anga Chit Male
ADD_PARTNER Chit Amba Female
ADD_CHILD Anga Ish Male
ADD_CHILD Anga Vich Male
ADD_PARTNER Vich Lika Female
ADD_CHILD Anga Aras Male
ADD_PARTNER Aras Chitra Female
ADD_CHILD Anga Satya Female
ADD_PARTNER Satya Vyan Male
GET_RELATIONSHIP Aras Siblings
GET_RELATIONSHIP Satya Partner
GET_RELATIONSHIP Lika Brother-In-Law
GET_RELATIONSHIP Lika Sister-In-Law