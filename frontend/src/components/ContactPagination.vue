<template>
  <nav aria-label="Page navigation" v-if="totalPages > 1">
    <ul class="pagination">
      <li class="page-item" :class="{ disabled: currentPage === 1 }">
        <button class="page-link" @click="$emit('page-changed', currentPage - 1)" :disabled="currentPage === 1">
          {{ $t('previous') }}
        </button>
      </li>
      <li class="page-item" v-for="page in totalPages" :key="page" :class="{ active: currentPage === page }">
        <button class="page-link" @click="$emit('page-changed', page)">{{ page }}</button>
      </li>
      <li class="page-item" :class="{ disabled: currentPage === totalPages }">
        <button class="page-link" @click="$emit('page-changed', currentPage + 1)"
          :disabled="currentPage === totalPages">
          {{ $t('next') }}
        </button>
      </li>
    </ul>
  </nav>
</template>

<script>
export default {
  name: 'ContactPagination',
  props: {
    totalRecords: {
      type: Number,
      required: true,
      default: 0,
    },
    pageSize: {
      type: Number,
      default: 10,
    },
    currentPage: {
      type: Number,
      default: 1,
    },
  },
  computed: {
    totalPages() {
      const totalRecords = this.totalRecords || 0;
      const pageSize = this.pageSize || 1;
      const pages = Math.ceil(totalRecords / pageSize);
      return isNaN(pages) || pages < 1 ? 1 : pages;
    },
  },
};
</script>

<style scoped>
.page-item.disabled .page-link {
  pointer-events: none;
}
</style>
