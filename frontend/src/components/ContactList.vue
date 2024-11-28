<template>
  <div class="container mt-4">
    <div class="row mb-3">
      <div class="col-md-6">
        <div class="input-group">
          <input v-model="searchFirstName" class="form-control" :placeholder="$t('searchFirstName')"
            @input="onSearchInput" />
          <input v-model="searchLastName" class="form-control" :placeholder="$t('searchLastName')"
            @input="onSearchInput" />
        </div>
      </div>
      <div class="col-md-6 text-end">
        <router-link to="/edit" class="btn btn-success">
          {{ $t('addNewContact') }}
        </router-link>
      </div>
    </div>

    <div class="table-responsive">
      <table class="table table-striped table-hover">
        <thead>
          <tr>
            <th @click="changeSort('FirstName')" class="sortable">
              {{ $t('firstName') }}
              <sort-indicator :column="'FirstName'" :current-sort="sortColumn" :current-order="sortOrder" />
            </th>
            <th @click="changeSort('LastName')" class="sortable">
              {{ $t('lastName') }}
              <sort-indicator :column="'LastName'" :current-sort="sortColumn" :current-order="sortOrder" />
            </th>
            <th @click="changeSort('PhoneNumber')" class="sortable">
              {{ $t('phoneNumber') }}
              <sort-indicator :column="'PhoneNumber'" :current-sort="sortColumn" :current-order="sortOrder" />
            </th>
            <th @click="changeSort('Email')" class="sortable">
              {{ $t('email') }}
              <sort-indicator :column="'Email'" :current-sort="sortColumn" :current-order="sortOrder" />
            </th>
            <th @click="changeSort('CreatedDateTime')" class="sortable">
              {{ $t('createdAt') }}
              <sort-indicator :column="'CreatedDateTime'" :current-sort="sortColumn" :current-order="sortOrder" />
            </th>
            <th @click="changeSort('LastModifiedDateTime')" class="sortable">
              {{ $t('lastModified') }}
              <sort-indicator :column="'LastModifiedDateTime'" :current-sort="sortColumn" :current-order="sortOrder" />
            </th>
            <th>{{ $t('actions') }}</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="contact in contacts" :key="contact.contactID">
            <td>{{ contact.firstName }}</td>
            <td>{{ contact.lastName }}</td>
            <td>{{ contact.phoneNumber }}</td>
            <td>{{ contact.email }}</td>
            <td>{{ formatDateTime(contact.createdDateTime) }}</td>
            <td>{{ formatDateTime(contact.lastModifiedDateTime) }}</td>
            <td>
              <div class="btn-group">
                <router-link :to="{ name: 'EditContact', params: { id: contact.contactID } }"
                  class="btn btn-sm btn-info">
                  {{ $t('edit') }}
                </router-link>
                <button @click="confirmDelete(contact.contactID)" class="btn btn-sm btn-danger ms-1">
                  {{ $t('delete') }}
                </button>
              </div>
            </td>
          </tr>
          <tr v-if="contacts.length === 0">
            <td colspan="7" class="text-center">{{ $t('noRecordsFound') }}</td>
          </tr>
        </tbody>
      </table>
    </div>

    <!-- Pagination -->
    <div class="d-flex justify-content-between align-items-center">
      <contact-pagination :total-records="totalRecords" :page-size="pageSize" :current-page="pageNumber"
        @page-changed="goToPage" />
      <div>
        {{ $t('recordsFound', { count: totalRecords }) }}
      </div>
    </div>
  </div>
</template>

<script>
import apiClient from '@/api/axios'
import { debounce } from 'lodash';
import ContactPagination from './ContactPagination.vue';

export default {
  name: 'ContactList',
  components: {
    ContactPagination,
  },
  data() {
    return {
      contacts: [],
      totalRecords: 0,
      pageNumber: 1,
      pageSize: 10,
      totalPages: 1,
      sortColumn: 'LastModifiedDateTime',
      sortOrder: 'DESC',
      searchFirstName: '',
      searchLastName: '',
      loading: false,
    };
  },
  methods: {
    formatDateTime(dateTime) {
      return new Date(dateTime).toLocaleString();
    },
    onSearchInput: debounce(function () {
      this.pageNumber = 1;
      this.fetchContacts();
    }, 300),
    async fetchContacts() {
      try {
        this.loading = true;
        const response = await apiClient.get('/Contacts', {
          params: {
            pageNumber: this.pageNumber,
            pageSize: this.pageSize,
            sortColumn: this.sortColumn,
            sortOrder: this.sortOrder,
            searchFirstName: this.searchFirstName || undefined,
            searchLastName: this.searchLastName || undefined,
          },
        });
        this.contacts = response.data;
        this.totalRecords = parseInt(response.headers['x-total-count'], 10) || 0;
        this.totalPages = Math.ceil(this.totalRecords / this.pageSize);
      } catch (error) {
        console.error('Error fetching contacts:', error);
        this.$toast.error(this.$t('errorOccurred'));
      } finally {
        this.loading = false;
      }
    },
    changeSort(column) {
      if (this.sortColumn === column) {
        this.sortOrder = this.sortOrder === 'ASC' ? 'DESC' : 'ASC';
      } else {
        this.sortColumn = column;
        this.sortOrder = 'ASC';
      }
      this.fetchContacts();
    },
    goToPage(page) {
      if (page >= 1 && page <= this.totalPages) {
        this.pageNumber = page;
        this.fetchContacts();
      }
    },
    async confirmDelete(contactID) {
      if (confirm(this.$t('confirmDelete'))) {
        try {
          await apiClient.delete(`/Contacts/${contactID}`);
          alert(this.$t('contactDeleted')); 
          await this.fetchContacts();
        } catch (error) {
          console.error('Error deleting contact:', error);
          alert(this.$t('errorOccurred'));  
        }
      }
    },
  },
  mounted() {
    this.fetchContacts();
  },
};
</script>

<style scoped>
.sortable {
  cursor: pointer;
  user-select: none;
}

.sortable:hover {
  background-color: rgba(0, 0, 0, 0.05);
}
</style>