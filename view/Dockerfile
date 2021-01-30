# get node version
FROM node:12.19.0

# working directory
WORKDIR /view

# add to path
ENV PATH /node_modules/.bin:$PATH

# npm installed
COPY package.json ./
COPY package-lock.json ./
RUN npm install

# copy the rest of the view
COPY . ./

# run
CMD ["npm", "start"]
