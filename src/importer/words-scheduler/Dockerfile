FROM mhart/alpine-node

RUN apk add --no-cache make gcc g++ python

# Install app dependencies
COPY package.json /src/package.json
#RUN npm install -g --save coffee-script@1.12.3 node-gyp@3.4.0
RUN cd /src; npm install

# Bundle app source
COPY . /src

USER nobody

EXPOSE  8080
CMD ["node", "/src/index.js"]