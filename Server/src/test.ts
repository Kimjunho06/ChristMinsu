import { Pool } from "./DB";

let sql = `SELECT (private_key) FROM (user) WHERE name = 'kwak1s1h';`;

async function test() {
    let result = await Pool.query(sql);
    console.log(result);
    console.log(typeof(result));
    console.log(typeof(result[0]));
}

test();