import nodeRSA from "node-rsa";
import crypto from "crypto";

export default class RSAManager
{
    eKey: nodeRSA;
    dKey: nodeRSA;
    publicKey: string = "";
    modulus: string;
    exponent: string;
    privateKey: string = "";

    constructor() {
        this.eKey = new nodeRSA({b:256});
        this.publicKey = this.eKey.exportKey('pkcs1-public-pem');
        this.privateKey = this.eKey.exportKey('pkcs1-private-pem');
        this.dKey = new nodeRSA(this.privateKey, 'pkcs1-private-pem');

        const pKey = crypto.createPublicKey(this.publicKey);
        let jwk = pKey.export({format: 'jwk'});
        this.modulus = jwk["n"] as string;
        this.exponent = jwk["e"] as string;

        console.log(this.publicKey, this.modulus, this.exponent);
    }

    encrypt(value: string): string {
        return this.eKey.encrypt(value, 'base64');
    }

    decrypt(value: string): string {
        return this.dKey.decrypt(value, 'utf8');
    }

}