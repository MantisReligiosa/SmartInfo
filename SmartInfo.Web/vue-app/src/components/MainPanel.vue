<template>
  <v-app-bar
      color="primary"
  >
    <v-menu
        open-on-hover
    >
      <template
          v-slot:activator="{ props }"
      >
        <v-app-bar-nav-icon
            variant="text"
            v-bind="props"
        ></v-app-bar-nav-icon>
      </template>
      <v-list>
        <v-list :items="mainMenu"></v-list>
      </v-list>
    </v-menu>
    <v-btn
        size="x-large"
    >
      <v-icon
          icon="mdi-navigation-variant-outline"
          class="mdi-flip-h"
          size="x-large"
      ></v-icon>
      <v-tooltip
          activator="parent"
          location="bottom"
      >{{ tooltipSelect }}</v-tooltip>
    </v-btn>
    <v-menu>
      <template v-slot:activator="{ props }">
        <v-btn
            size="x-large"
            v-bind="props"
        >
          <v-icon
              icon="mdi-format-text"
              size="x-large"
          ></v-icon>
          <v-icon
              icon="mdi-chevron-down"
          ></v-icon>
          <v-tooltip
              activator="parent"
              location="bottom"
          >{{ tooltipBlocks }}</v-tooltip>
        </v-btn>
      </template>
      <v-list>
        <v-list :items="blocksMenu"></v-list>
      </v-list>
    </v-menu>
    <v-spacer></v-spacer>
    <v-menu>
      <template v-slot:activator="{ props }">
        <v-btn
            size="x-large"
            v-bind="props"
        >
          Устройство1
          <v-icon
              icon="mdi-chevron-down"
          ></v-icon>
        </v-btn>
      </template>
      <v-list>
        <v-list :items="devices"></v-list>
      </v-list>
    </v-menu>
    <v-spacer></v-spacer>
    <v-btn
        variant="plain"
    >
      <v-icon
          size="x-large"
          icon="mdi-play"
      ></v-icon>
      <v-tooltip
          activator="parent"
          location="bottom"
      >{{ tooltipPlay }}</v-tooltip>
    </v-btn>
    <v-btn
        variant="plain"
    >
      <v-icon
          size="x-large"
          icon="mdi-stop"
      ></v-icon>
      <v-tooltip
          activator="parent"
          location="bottom"
      >{{ tooltipStop }}</v-tooltip>
    </v-btn>
    <v-btn
        variant="plain"
    >
      <v-icon
          size="x-large"
          icon="mdi-refresh"
      ></v-icon>
      <v-tooltip
          activator="parent"
          location="bottom"
      >{{ tooltipRefresh }}</v-tooltip>
    </v-btn>
  </v-app-bar>
</template>

<script>
export default {
  data: () => ({
    tooltipSelect: 'Выбор',
    tooltipBlocks: 'Блоки',
    tooltipPlay: 'Пуск',
    tooltipStop: 'Стоп',
    tooltipRefresh: 'Перезагрузить',
    mainMenu: [
      {
        title: 'Item #1',
        value: 1,
      },
      {
        title: 'Item #2',
        value: 2,
      },
      {
        title: 'Item #3',
        value: 3,
      },
    ],
    blocksMenu: [
      {
        title: 'Текст',
        value: 1,
        props: {
          prependIcon: 'mdi-format-text',
        },
      },
      {
        title: 'Таблица',
        value: 2,
        props: {
          prependIcon: 'mdi-table',
        },
      },
      {
        title: 'Изображение',
        value: 3,
        props: {
          prependIcon: 'mdi-image',
        },
      },
      {
        title: 'Сценарий',
        value: 4,
        props: {
          prependIcon: 'mdi-movie-open-outline',
        },
      },
    ],
    // devices: [
    //   {
    //     title: 'Устройство 1',
    //     value: 1,
    //   },
    //   {
    //     title: 'Устройство 2',
    //     value: 3,
    //   },
    // ]
  }),
  computed:{
    devices() {
      return this.$store.getters.devicesList
    }
  }
}
</script>

<style scoped>

</style>