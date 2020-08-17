To make the service run correctly, pls follow the rules below:

1. Copy the configuration file to the folder where the Service app is located

2. Set the permission to the local system in DCOM, 
	run dcomcnfg
	in COM Sercurity set the limit permission
3. Open Service and set the service aoucunt to "Local system"

4. Config the SQL, 
	- Open Sql Manager Studio 
	- Security,login, set acount NT AUTHORITY\SYSTEM add server roles ==> sysadmin
	- Set the login account to login to SQL in config file (must)