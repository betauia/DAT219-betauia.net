module.exports = {
    devServer: {
        port: 8080,
        proxy: {
            '^/api': {
                target: 'https://betauia-backend.azurewebsites.net',
                ws: true,
                changeOrigin: true,
            },
        },
    }
}
