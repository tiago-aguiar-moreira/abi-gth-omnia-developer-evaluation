[Back to README](../README.md)

### Users

#### GET /users
- Description: Retrieve a list of all users
- Query Parameters:
  - `_page` (optional): Page number for pagination (default: 1)
  - `_size` (optional): Number of items per page (default: 10)
  - `_order` (optional): Ordering of results (e.g., "username asc, email desc")
- Response: 
  ```json
  {
    "data": [{
        "id": "integer",
        "email": "string",
        "username": "string",
        "password": "string",
        "address": {
          "city": "string",
          "street": "string",
          "number": "integer",
          "zipcode": "string",
          "geolocation": {
            "lat": "decimal",
            "long": "decimal"
          }
        },
        "phone": "string",
        "status": "integer (1: Active, 2: Inactive, 3: Suspended)",
        "role": "integer (1: Customer, 2: Manager, 3: Admin)"
      }
    ],
    "totalItems": "integer",
    "currentPage": "integer",
    "totalPages": "integer"
  }
  ```

#### POST /users
- Description: Add a new user
- Request Body:
  ```json
  {
    "email": "string",
    "username": "string",
    "password": "string",
    "address": {
      "city": "string",
      "street": "string",
      "number": "integer",
      "zipcode": "string",
      "geolocation": {
        "lat": "decimal",
        "long": "decimal"
      }
    },
    "phone": "string",
    "status": "integer (1: Active, 2: Inactive, 3: Suspended)",
    "role": "integer (1: Customer, 2: Manager, 3: Admin)"
  }
  ```
- Response: 
  ```json
  {
    "data": {
      "id": "integer",
      "email": "string",
      "username": "string",
      "address": {
        "city": "string",
        "street": "string",
        "number": "integer",
        "zipcode": "string",
        "geolocation": {
          "lat": "decimal",
          "long": "decimal"
        }
      },
      "phone": "string",
      "status": "integer (1: Active, 2: Inactive, 3: Suspended)",
      "role": "integer (1: Customer, 2: Manager, 3: Admin)"
    },
    "success": true,
    "message": "",
    "errors": []
  }
  ```

#### GET /users/{id}
- Description: Retrieve a specific user by ID
- Path Parameters:
  - `id`: User ID
- Response: 
  ```json
  {
    "data": {
      "id": "integer",
      "email": "string",
      "username": "string",
      "password": "string",
      "address": {
        "city": "string",
        "street": "string",
        "number": "integer",
        "zipcode": "string",
        "geolocation": {
          "lat": "decimal",
          "long": "decimal"
        }
      },
      "phone": "string",
      "status": "integer (1: Active, 2: Inactive, 3: Suspended)",
      "role": "integer (1: Customer, 2: Manager, 3: Admin)"
    },
    "success": true,
    "message": "",
    "errors": []
  }
  ```

#### PUT /users/{id}
- Description: Update a specific user
- Path Parameters:
  - `id`: User ID
- Request Body:
  ```json
  {
    "email": "string",
    "username": "string",
    "password": "string",
    "address": {
      "city": "string",
      "street": "string",
      "number": "integer",
      "zipcode": "string",
      "geolocation": {
        "lat": "decimal",
        "long": "decimal"
      }
    },
    "phone": "string",
    "status": "integer (1: Active, 2: Inactive, 3: Suspended)",
    "role": "integer (1: Customer, 2: Manager, 3: Admin)"
  }
  ```
- Response: 
  ```json
  {
    "data":
    {
      "id": "integer",
      "email": "string",
      "username": "string",
      "address": {
        "city": "string",
        "street": "string",
        "number": "integer",
        "zipcode": "string",
        "geolocation": {
          "lat": "decimal",
          "long": "decimal"
        }
      },
      "phone": "string",
      "status": "integer (1: Active, 2: Inactive, 3: Suspended)",
      "role": "integer (1: Customer, 2: Manager, 3: Admin)"
    },
    "success": true,
    "message": "",
    "errors": []
  }
  ```

#### DELETE /users/{id}
- Description: Delete a specific user
- Path Parameters:
  - `id`: User ID
- Response: 
  ```json
  
  
  ```
<br/>
<div style="display: flex; justify-content: space-between;">
  <a href="./carts-api.md">Previous: Carts API</a>
  <a href="./auth-api.md">Next: Auth API</a>
</div>
