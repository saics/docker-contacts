#!/bin/bash

# Start SQL Server in the background
/opt/mssql/bin/sqlservr &

# Wait for SQL Server to start
echo "Waiting for SQL Server to start..."

# Wait until SQL Server is ready to accept connections
until /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'YourStrong@Passw0rd' -Q "SELECT 1" > /dev/null 2>&1
do
  echo -n "."
  sleep 1
done

# Check if the database files exist
if [ ! -f /var/opt/mssql/data/ContactsDB.mdf ]; then
    echo "Database not found. Restoring the database..."
    # Perform the database restore
    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'YourStrong@Passw0rd' -Q "
    RESTORE DATABASE ContactsDB
    FROM DISK = '/var/opt/mssql/backup/ContactsDB.bak'
    WITH MOVE 'ContactsDB' TO '/var/opt/mssql/data/ContactsDB.mdf',
    MOVE 'ContactsDB_log' TO '/var/opt/mssql/data/ContactsDB_log.ldf'
    "
else
    echo "Database already exists. Skipping restore."
fi

# Keep the script running to prevent the container from exiting
wait
