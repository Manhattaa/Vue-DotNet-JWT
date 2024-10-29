<template>
  <br>
  <div>
    <form @submit.prevent="login">
      <input type="text" placeholder="Username"><br>
      <input type="password" placeholder="Password"><br>
      <button type="submit">Login</button>
    </form>
  </div>
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {
      error: null
    }
  },
  methods: {
    async login() {
  try {
    const response = await axios.get("https://localhost:7069/auth/login", {
      withCredentials: true
    });
    console.log(response.data);
    localStorage.setItem("token", response.data.token);
  } catch (error) {
    console.error("Error during login request:", error);
      }
    }
  }
}
</script>