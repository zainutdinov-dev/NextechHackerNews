const express = require('express');
const path = require('path');

const app = express();
const port = process.env.PORT || 8080;  // Use the port defined by Azure Web App

// Serve static files from the Angular build (in 'dist' folder)
app.use(express.static(path.join(__dirname, 'browser')));

// Handle all other routes and serve the Angular app's index.html
app.get('*', (req, res) => {
    res.sendFile(path.join(__dirname, 'browser/index.html'));
});

// Start the server
app.listen(port, () => {
    console.log(`Server is running on port ${port}`);
});