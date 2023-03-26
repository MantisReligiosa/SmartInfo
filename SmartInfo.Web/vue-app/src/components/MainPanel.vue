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
      >{{ tooltipSelect }}
      </v-tooltip>
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
          >{{ tooltipBlocks }}
          </v-tooltip>
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
          {{ selectedDevice.name }}
          <v-icon
              icon="mdi-chevron-down"
          ></v-icon>
        </v-btn>
      </template>
      <v-list>
        <v-list-item
            v-for="(device, index) in devices"
            @click="selectDeviceById(device.id)"
            :title="device.name"
            :key="index"
        ></v-list-item>
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
      >{{ tooltipPlay }}
      </v-tooltip>
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
      >{{ tooltipStop }}
      </v-tooltip>
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
      >{{ tooltipRefresh }}
      </v-tooltip>
    </v-btn>
  </v-app-bar>
</template>

<script>
import {blockType, blockIcon} from '@/constants'
import {mainStore} from '@/store/mainStore'
import {deviceStore} from '@/store/deviceStore'

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
        value: blockType.text,
        props: {
          prependIcon: blockIcon.text,
        },
      },
      {
        title: 'Таблица',
        value: blockType.table,
        props: {
          prependIcon: blockIcon.table,
        },
      },
      {
        title: 'Изображение',
        value: blockType.picture,
        props: {
          prependIcon: blockIcon.picture,
        },
      },
      {
        title: 'ДатаВремя',
        value: blockType.dateTime,
        props: {
          prependIcon: blockIcon.dateTime,
        },
      },
      {
        title: 'Сценарий',
        value: blockType.scenario,
        props: {
          prependIcon: blockIcon.scenario,
        },
      },
    ],
  }),
  methods: {
    async selectDeviceById(deviceId) {
      mainStore().selectDeviceById(deviceId)
      await deviceStore().loadById(deviceId)
    }
  },
  computed: {
    devices() {
      return mainStore().devices
    },
    selectedDevice() {
      return mainStore().device
    }
  }
}
</script>

<style scoped>

</style>