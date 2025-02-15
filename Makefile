dev:
	dotnet watch run

up:
	docker-compose up -d

down:
	docker-compose down --remove-orphans