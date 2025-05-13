window.previewImage = (inputElem, imgElem) => {
  debugger;
  const url = URL.createObjectURL(document.getElementsByTagName('input')[0].files[0]);
  imgElem.addEventListener('load', () => URL.revokeObjectURL(url), { once: true });
  imgElem.src = url;
}
