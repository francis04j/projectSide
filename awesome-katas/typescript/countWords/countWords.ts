/**
 * returns the count of words in a string.

 * A word is defined as any group of one or more alphanumeric characters separated by a space.

 * Example:

 * An input value of "Hello, World!" should return 2.

 * @param input 
 */
export const countWords = (input: string) => {
 
    let separator = ' '; 
    let result = input.split(separator)
    let countExcludeSpace = 0;
   
    result.forEach(elem => { 
        if(!isNaN(elem.charCodeAt(0))) {countExcludeSpace++; }});
    return countExcludeSpace;
}

// LEarnt to day
// input split inclide space
//  //" " == 0)  // true
