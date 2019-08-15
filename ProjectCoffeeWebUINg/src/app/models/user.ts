import { DrinkExtra } from './drinkextra';

export class User {
    constructor(public userId: string,
        public userName: string,
        public drinkCode: string,
        public cupSizeCode: string,
        public milkTypeCode: string,
        public extras?: DrinkExtra[]) {}
}