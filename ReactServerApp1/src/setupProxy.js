const { createProxyMiddleware } = require('http-proxy-middleware');

module.exports = function (app) {
    app.use(
        '/api',
        createProxyMiddleware({
            //target: 'https://localhost:7150',
            target: 'https://localhost:8085',
            changeOrigin: true,
            secure: false
        })
    );
};
