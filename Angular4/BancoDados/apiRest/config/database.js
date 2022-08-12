module.exports = {
    hrPool: {
      user: process.env.HR_USER || "Globus",
      password: process.env.HR_PASSWORD || "o0wn33e3rnovo",
      connectString: process.env.HR_CONNECTIONSTRING || "Globusserver",
      poolMin: 10,
      poolMax: 10,
      poolIncrement: 0
    }
  };