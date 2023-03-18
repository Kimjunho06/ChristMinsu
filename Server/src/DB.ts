import MySQL from "mysql2/promise";
import fs from "fs";
import path from "path";

const poolOption: MySQL.PoolOptions = {
    host: '127.0.0.1',
    port: 3306,
    user: 'root',
    password: 'tjrgus12*!',
    database: 'christminsu',
    connectionLimit: 10
};

export const Pool = MySQL.createPool(poolOption);