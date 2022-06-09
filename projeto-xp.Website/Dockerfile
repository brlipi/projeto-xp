#FROM composer as composer
#COPY website/composer.json /app
#RUN composer install --ignore-platform-reqs --no-scripts

FROM php:7.2-apache
#COPY --from=composer:2 /usr/bin/composer /usr/bin/composer
COPY website/ /var/www/html/
#RUN apt-get update; \
    #apt-get install -y git zip unzip;
#RUN a2enmod rewrite; a2enmod actions
#WORKDIR /var/www/html
#COPY --from=composer /app/vendor /var/www/root/vendor
#COPY composer.json .
#COPY composer.lock .
#RUN composer install --no-dev
