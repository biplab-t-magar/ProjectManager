const ConvertTime = (date) => {
    var convertedDate = new Date(Date.parse(date));
    return convertedDate.toTimeString();
}

export default ConvertTime;