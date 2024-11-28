<template>
  <div>
    <div class="language-switcher">
      <button @click="changeLocale('en')" :disabled="currentLocale === 'en'">English</button>
      <button @click="changeLocale('hr')" :disabled="currentLocale === 'hr'">Hrvatski</button>
    </div>
    <router-view></router-view>
  </div>
</template>

<script>
export default {
  computed: {
    currentLocale() {
      return this.$i18n.locale;
    },
  },
  methods: {
    changeLocale(locale) {
      this.$i18n.locale = locale;
    },
  },
};
</script>

<style>
.language-switcher {
  padding: 10px;
}
.language-switcher button {
  margin-right: 5px;
}
</style>
