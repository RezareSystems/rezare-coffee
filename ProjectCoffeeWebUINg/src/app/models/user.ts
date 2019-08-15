import { DrinkExtra } from './drinkextra';

export class User {
    constructor(public userId: string,
        public UserName: string,
        public DrinkCode: string,
        public CupSizeCode: string,
        public MilkTypeCode: string,
        public Extras?: DrinkExtra[]) {}
}