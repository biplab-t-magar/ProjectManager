/**/
/*
 * This file contains the utility function to convert C# Style date-time values into easily readable dates
 * / 
/**/

const ConvertDate = (date) => {
    var convertedDate = new Date(Date.parse(date));
    return convertedDate.toDateString();
}

export default ConvertDate;