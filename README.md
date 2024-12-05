<h1  align="center">Restaurant Information System for Relaxing Koala</h1>
<h3  align="center">SWE30003 - Software Architectures and Design</h3>
<p  align="center">
<img  src="https://github.com/user-attachments/assets/004ea4db-db6e-488c-bb06-a02e48e2120a"  alt="Homepage screenshot">
</p>

## Table of Contents

- [Introduction](#introduction)

- [Features](#features)

- [Technologies Used](#technologies-used)

- [Getting Started](#getting-started)

- [Acknowledgements](#acknowledgements)

## Introduction

This repository contains the **Restaurant Information System**, which was developed as part of a course project. The system is designed for **The Relaxing Koala**, a recently expanded restaurant and café that has grown to accommodate a larger customer base and operations.

The primary goal of this project is to develop an efficient solution to manage the restaurant's key processes, such as reservations, menu viewing, and take-away orders.

## Features

### General Users:

- Login and Register: Create an account and securely log in to access the system.

- View Menu: Browse the full menu with pricing.

- Unauthorized Access Handling: If a user attempts to access a page or feature they are not authorized for, they will be automatically redirected to an **Unauthorized Access** page.

### Customers:

- Make Reservation Requests: Submit a request to book a table at the restaurant.

- Place Orders: Place orders directly from the system for dine-in or take-away.

### Staff:

- Access management pages to handle daily operations.

- Restricted from user management.

### Admin:

Have full access to all the features, including user management.

## Technologies Used

- **`ASP.NET Core`**: Version 8.0

- **`Razor Pages`**

- **`SQLite`**: Version 3.47.0

- **`Entity Framework Core`**: Version 8.0.11

- **`Microsoft Visual Studio Web Code Generation Design`**: Version 8.0.7

## Getting Started

### Prerequisites

- .NET SDK 8.0

### Installation

#### 1. Clone the repository:

- Open terminal and run

```bash
git clone https://github.com/SWE30003-G5/SWE30003_Group5_Koala.git
```

- Go to the project directory:

```bash
cd PATH/TO/SWE30003_Group5_Koala
```

#### 2. Restore dependencies:

```bash
dotnet restore
```

#### 3. Run the Application:

- Start the development server:

```bash
dotnet run
```

- Open your browser and navigate to the URL provided on the terminal:

```bash
https://localhost:PORT_NUMBER
```

_The PORT_NUMBER is typically displayed in the terminal when you run **dotnet run**. Common values are 7123 or 5017, depending on the configuration._

## License

Distributed under the MIT License. See the [LICENSE.txt](./LICENSE.txt) file for more information.

## Contact

If you have any questions or feedback, feel free to reach out!

- **Project lead**: [Thái Dương Bảo Tân](https://github.com/baotan1909).

- **Menu Page and Authentication Pages Developer**: [Lê Hữu Nhân](https://github.com/JJWilson-75).

- **Idea Provider and Concept Contributor**: [Đặng Quỳnh Chi](https://github.com/Chi-Quynh).

## Acknowledgements

The image displayed on the homepage, featuring Komala from Pokémon TCG, is the property of **Nintendo**. We do not own the image, and it is used for illustrative, non-commercial purposes only. All rights to the image belong to its respective copyright holders. For more details, you can view the official Pokémon card at [Komala | SM—Guardians Rising | TCG Card Database](https://www.pokemon.com/us/pokemon-tcg/pokemon-cards/series/sm2/114/).
