FROM node:11
WORKDIR /app

COPY package*.json ./

RUN yarn install

COPY ./ ./

RUN yarn build

FROM nginx

RUN mkdir /app
COPY --from=0 /app/dist /app
COPY nginx.conf /etc/nginx/nginx.conf

EXPOSE 80
EXPOSE 8080
CMD ["nginx", "-g", "daemon off;"]
