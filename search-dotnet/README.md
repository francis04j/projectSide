

# Build 
docker build -t francis04j/justeatapi .

# Run (with docker)
 docker run -d --name jsp --rm -p 8080:80 francis04j/justeatapi (example: ports can be changed to suit your need)
 Browse to localhost:8080/swagger/index.html
 OR
 curl -v http://localhost:8080/api/search?id=${POST_CODE}

# Run (without docker)
$ cd Api
$ dotnet run
Browse to: localhost:5001/swagger/index.html

# Possible improvements 
 - use RestClient, Automapper
 - Include more tests
 - use paging
 - use Caching
 