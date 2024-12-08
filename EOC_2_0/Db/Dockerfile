FROM mcr.microsoft.com/mssql/server:2022-latest

# Копируем файлы и скрипты и определяем их
COPY init.sql ./docker-entrypoint-initdb.d/
COPY entrypoint.sh ./docker-entrypoint-initdb.d/
COPY import-data.sh ./docker-entrypoint-initdb.d/

# Переключаемся на root для изменения разрешений
USER root

# Даем права на исполняемый файл
RUN chmod +x ./docker-entrypoint-initdb.d/entrypoint.sh

# Возвращаемся обратно к пользователю mssql
USER mssql

# Говорим Docker, чтобы использовал наш entrypoint
ENTRYPOINT ["/bin/bash", "./docker-entrypoint-initdb.d/entrypoint.sh"]

