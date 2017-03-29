FROM alpine

# Update
RUN apk add --update nodejs python make gcc g++

# Removing logs
RUN npm config set loglevel warn

# Install app dependencies
COPY package.json /src/package.json
RUN npm install -g --save coffee-script@1.12.3 node-gyp@3.4.0
RUN cd /src; npm install

# Bundle app source
COPY . /src

EXPOSE  8080
CMD ["node", "/src/index.js"]
