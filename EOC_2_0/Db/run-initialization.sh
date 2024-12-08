# Wait to be sure that SQL Server came up
sleep 90s

# Run the setup script to create the DB and the schema in the DB
# Note: make sure that your password matches what is in the Dockerfile
/opt/mssql-tools/bin/sqlcmd -S SqlServerDb -U sa -P 123videninSERG13 -d EOC_2_0 -i init.sql
