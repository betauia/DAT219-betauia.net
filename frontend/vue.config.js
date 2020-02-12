module.exports = {
    devServer: {
	https:true,
        port: 443,
        proxy: {
            '^/api': {
                target: 'http://localhost:5000',
                ws: true,
                changeOrigin: true,
            },
        },
    }
}
