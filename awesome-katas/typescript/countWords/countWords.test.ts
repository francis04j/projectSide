import { countWords } from "./countWords";
import  NotImplementedException from '../Exceptions/NotImplementedException';

describe('countWords', () => {
    // EXAMPLE: how to throw and use exception in test
    // it('should return 0 for empty string', () => {
    //     expect(() => {
    //         countWords('');
    //     }).toThrow(NotImplementedException)
    // })

    it('should return 0 for an empty word in string', () => {
        expect(countWords('')).toEqual(0);
    })

    it('should return 1 for a word in string', () => {
        expect(countWords('aword')).toEqual(1);
    })

    it('should ignore space', () => {
        expect(countWords(' aword ')).toEqual(1);
    })

    it('should count words with numbers', () => {
        expect(countWords(' aword 123')).toEqual(2);
    })

    it('should ignore special characters with numbers', () => {
        expect(countWords(' aword 123 @£$')).toEqual(2);
    })
})