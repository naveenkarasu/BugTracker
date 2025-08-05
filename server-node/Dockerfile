FROM node:18-alpine AS base
WORKDIR /app
COPY package*.json ./

FROM base AS dependencies
RUN npm ci --only=production

FROM base AS build
RUN npm ci
COPY . .

FROM base AS production
COPY --from=dependencies /app/node_modules ./node_modules
COPY --from=build /app ./
RUN mkdir -p logs

EXPOSE 4000
CMD ["npm", "start"] 