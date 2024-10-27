<template>
</template>

<script>
export default {
  name: 'Logout',
  mounted() {
    localStorage.removeItem('token');
    this.$router.push('/');
  }
}
</script>