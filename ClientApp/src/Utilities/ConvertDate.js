const ConvertDate = (date) => {
    var convertedDate = new Date(Date.parse(date));
    return convertedDate.toDateString();
}

export default ConvertDate;