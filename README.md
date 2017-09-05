# DatabaseCompare

A simple Tool to find differences between multiple Databases running the (mostly) same schema.

## When to use
Use this program, when you have at least 3 schemas you want to compare to each other. If you only want to compare two databases against each other there is better tooling available. For PostgreSQL have a look at [apgdiff](https://www.apgdiff.com/)

Currently the following schema fetchers are implemented:

* MySQL
* PostgreSQL

## What it does

The program fetches a lot of schema data from the configured databases (Tables, Columns, Primary Keys, Check Constraints (only on PostgreSQL)). The schema data will be stored in a SQLite Database where all analysis is based on.

The implemented analysis shows you differences in column definition within the given databases. You are free to run further analysis on the metadata database.

## Configuration

All configuration happens within `App.config`. Here is the definition of the keys:

* `RemoteDataProvider` (String) Type of the remote database (allowed are: `mysql` and `postgres`)
* `RemoteDataConnectionString` (String) Valid Connection string which will be sent to the database connection class
* `RemoteDataDatabaseRegex` (String) A Regex to filter all databases that should be queried
* `RemoteDataFilterSchema` (Boolean) Are all schema inside a database to be fetched?
* `RemoteDataSchemaList` (Array of String) Lists the schemas that should be queried (needs `RemoteDataFilterSchema` to be `true`)


# License

DatabaseCompare (c) by Bernhard Wendel

DatabaseCompare is licensed under a Creative Commons Attribution-NonCommercial 4.0 International License.

You should have received a copy of the license along with this work. If not, see <http://creativecommons.org/licenses/by-nc/4.0/>.