export default function pgFormatDate(date) {
  function zeroPad(d) {
    return ("0" + d).slice(-2)
  }
  var parsed = new Date(date)
  return zeroPad(parsed.getDate()) 
    + '.' + zeroPad(parsed.getMonth() + 1) 
    + '.' + (parsed.getUTCFullYear())
    + ' ' + zeroPad(parsed.getHours()) + ':' + zeroPad(parsed.getMinutes()) + ':' + zeroPad(parsed.getSeconds());
}