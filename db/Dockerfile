# Use the base SQL Server image
FROM mcr.microsoft.com/mssql/server:2022-latest

# Switch to root user to install additional software
USER root

# Install prerequisites and sqlcmd (adjust as needed)
RUN apt-get update && \
    apt-get install -y curl apt-transport-https gnupg && \
    curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - && \
    curl https://packages.microsoft.com/config/ubuntu/20.04/prod.list > /etc/apt/sources.list.d/mssql-release.list && \
    apt-get update && \
    ACCEPT_EULA=Y apt-get install -y msodbcsql17 mssql-tools && \
    apt-get clean && rm -rf /var/lib/apt/lists/*

# Add mssql-tools to PATH environment variable
ENV PATH="$PATH:/opt/mssql-tools/bin"

# Copy the initialization script
COPY init-db.sh /init-db.sh
RUN chmod +x /init-db.sh

# Switch back to the mssql user
USER mssql

# Set the entrypoint to the initialization script
ENTRYPOINT ["/bin/bash", "/init-db.sh"]
