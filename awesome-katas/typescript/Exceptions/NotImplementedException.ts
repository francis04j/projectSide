export default class NotImplementedException extends Error {
    constructor(message?: string){
        super(message || 'Not Implemented');
        this.name = 'Not ImplementedException'
    }
}