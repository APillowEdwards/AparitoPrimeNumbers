# Aparito Prime Numbers

This is my technical test for Aparito, it's an app that generates prime numbers using a .NET Core application and displays them on a React frontend.

In order to run it using docker, simply clone the repository, navigate to the root of the repository and run:

    docker-compose up --build

If ports 80 or 8080 aren't available, you just need to amend 'docker-compose.yml', replacing whichever application's port value and the corresponding environment variable in the other application. 
