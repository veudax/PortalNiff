import { Injectable} from '@angular/core';
import * as CryptoJS from 'crypto-js';

@Injectable()
export class Criptografia {
    cripTxt: any;

    DADOS_CRIPTOGRAFAR = {
        algoritmo : "aes256",
        segredo : "chaves"
        };

  criptografar(texto: string){
    this.cripTxt = CryptoJS.AES.encrypt(texto, this.DADOS_CRIPTOGRAFAR.segredo).toString();
    return this.cripTxt;
  }

  
  descriptografar(texto: string){
    this.cripTxt = CryptoJS.AES.decrypt(texto, this.DADOS_CRIPTOGRAFAR.segredo).toString(CryptoJS.enc.Utf8);
    return this.cripTxt;
  }

  //npm install crypto-js --s
  //npm install @types/crypto-js
  // no arquivo systemjs.config.js incluir
  // no map -> 'crypto-js': 'npm:crypto-js/crypto-js.js',
}