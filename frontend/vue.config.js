module.exports = {
  devServer: {
    proxy: {
      '/api': {
        target: 'http://localhost:2900',
      },
      '/dummy': {
        target: 'https://jsonplaceholder.typicode.com/posts',
      },
    },
  },
};
