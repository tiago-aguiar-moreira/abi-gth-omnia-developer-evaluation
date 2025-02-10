[Back to README](../README.md)

### Sales

#### GET /sales
- Description: Retrieve a list of all sales
- Query Parameters:
  - `_page` (optional): Page number for pagination (default: 1)
  - `_size` (optional): Number of items per page (default: 10)
  - `_order` (optional): Ordering of results (e.g., "id desc, userId asc")
- Response: 
  ```json
  {
    "data": [
      {
        "id": "string (guid)",
        "saleNumber": "integer",
        "saleDate": "string (date)",
        "userId": "string (guid)",
        "totalAmount": "decimal",
        "branch": "string",
        "status": "integer",
        "products": [
          {
            "productId": "string (guid)",
            "quantity": "integer",
            "unitPrice": "decimal",
            "discount": "decimal"
          }
        ]
      }
    ],
    "totalItems": "integer",
    "currentPage": "integer",
    "totalPages": "integer"
  }
  ```

#### POST /sales
- Description: Add a new sale
- Request Body:
  ```json
  {
    "saleNumber": "integer",
    "saleDate": "string (date)",
    "userId": "string (guid)",
    "branch": "string",
    "status": "integer (enum: 1 - Active, 2 - Canceled)",
    "products": [
      {
        "productId": "string (date)",
        "quantity": "integer"
      }
    ]
  }
  ```
- Response: 
  ```json
  {
    "Id": "string (guid)",
    "saleNumber": "integer",
    "saleDate": "string (date)",
    "userId": "string (guid)",
    "totalAmount": "decimal",
    "branch": "string",
    "status": "integer (enum: 1 - Active, 2 - Canceled)",
    "products": [
      {
        "productId": "string (guid)",
        "quantity": "integer",
        "unitPrice": "decimal",
        "discount": "decimal"
      }
    ]
  }
  ```

#### GET /sales/{id}
- Description: Retrieve a specific sale by ID
- Path Parameters:
  - `id`: Sale ID
- Response: 
  ```json
  {
    "Id": "string (guid)",
    "saleNumber": "integer",
    "saleDate": "string (date)",
    "userId": "string (guid)",
    "totalAmount": "decimal",
    "branch": "string",
    "status": "integer (enum: 1 - Active, 2 - Canceled)",
    "products": [
      {
        "productId": "string (guid)",
        "quantity": "integer",
        "unitPrice": "decimal",
        "discount": "decimal"
      }
    ]
  }
  ```

#### PUT /sales/{id}
- Description: Update a specific sale
- Path Parameters:
  - `id`: Sale ID
- Request Body:
  ```json
  {
    "saleNumber": "integer",
    "saleDate": "string (date)",
    "userId": "string (guid)",
    "branch": "string",
    "status": "integer (enum: 1 - Active, 2 - Canceled)",
    "products": [
      {
        "productId": "string (guid)",
        "quantity": "integer"
      }
    ]
  }
  ```
- Response: 
  ```json
  {
    "Id": "string (guid)",
    "saleNumber": "integer",
    "saleDate": "string (date)",
    "userId": "string (guid)",
    "totalAmount": "decimal",
    "branch": "string",
    "status": "integer (enum: 1 - Active, 2 - Canceled)",
    "products": [
      {
        "productId": "string (guid)",
        "quantity": "integer",
        "unitPrice": "decimal",
        "discount": "decimal"
      }
    ]
  }
  ```

#### DELETE /sales/{id}
- Description: Delete a specific sale
- Path Parameters:
  - `id`: Sale ID
- Response: 
  ```json
  {
    "message": "string"
  }
  ```


<br>
<div style="display: flex; justify-content: space-between;">
  <a href="./users-api.md">Previous: Users API</a>
  <a href="./project-structure.md">Next: Project Structure</a>
</div>