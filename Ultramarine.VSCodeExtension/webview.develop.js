function getWebView() {
  return `<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
				<meta name="viewport" content="width=device-width,initial-scale=1,shrink-to-fit=no">
				<meta name="theme-color" content="#000000">
        <title>Ultramarine</title>
        <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
  </head>
  <body>
    <noscript>You need to enable JavaScript to run this app.</noscript>
    <div id="root"></div>
    <script src="http://localhost:64825/assets/bundle.js"></script>
  </body>
</html>`
}

module.exports = getWebView
