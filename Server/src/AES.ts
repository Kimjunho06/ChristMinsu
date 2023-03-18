import crypto from "crypto";

let key1 = crypto.scryptSync("nwieown@(0nn8#1721#BN", "kwak", 24);
console.log(key1.toString('hex'));

//let iv = crypto.randomFillSync(new Uint8Array(16));
//console.log(iv.toString());
let iv = crypto.randomBytes(16);

const cipher = crypto.createCipheriv('aes-192-cbc', key1, iv);
cipher.setEncoding('hex');

let result = '';

cipher.on('data', (chunk) => {
    result += chunk;
});
cipher.on('end', () => console.log(result));

cipher.write('password*!');
cipher.end();

const decipher = crypto.createDecipheriv('aes-192-cbc', key1, iv);

let decrypted = '';
decipher.on('readable', () => {
  let chunk;
  while (null !== (chunk = decipher.read())) {
    decrypted += chunk.toString('utf8');
  }
});
decipher.on('end', () => {
  console.log(decrypted);
  // Prints: some clear text data
});

// Encrypted with same algorithm, key and iv.
const encrypted = result;
decipher.write(encrypted, 'hex');
decipher.end();