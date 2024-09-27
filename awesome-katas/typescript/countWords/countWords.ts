/**
 * returns the count of words in a string.

 * A word is defined as any group of one or more alphanumeric characters separated by a space.

 * Example:

 * An input value of "Hello, World!" should return 2.

 * @param input 
 */

function isAlphanumeric(str: string) {
    return /^[a-zA-Z0-9]+$/.test(str);
}

export const countWords = (input: string) => {
    let separator = ' ';
    return input.split(separator)
        .filter(x => isAlphanumeric(x))
        .length;
}

// LEarnt to day
// input split inclide space
//  //" " == 0)  // true
