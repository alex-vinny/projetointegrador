### STAGE 1: Build ###
FROM node AS build
WORKDIR /src
COPY package.json package-lock.json ./
RUN npm cache clean --force
RUN npm i
COPY . .
RUN npm run build

### STAGE 2: Run ###
FROM nginx
### Do note the project name, as 'ng build or npm run build'
### will create the directory structure like this
### /dist/your-project-name
COPY --from=build /src/dist/app /usr/share/nginx/html