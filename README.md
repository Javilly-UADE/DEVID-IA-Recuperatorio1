# Recuperatorio Entrega 1

## Objetivo

En este recuperatorio se entrega una escena base en Unity con:

- Un **Player** controlable
- Un **Enemy 1** que utiliza una **FSM**
- Un **Enemy 2** que utiliza un **Decision Tree**
- Un sistema de **Line of Sight**
- Un archivo de **SteeringBehaviours**
- Obstáculos en la escena

La escena **no está terminada**. El objetivo es que puedan **corregir e integrar** los sistemas vistos en clase para que ambos enemigos funcionen correctamente.

---

## Qué se espera que hagan

### 1. Corregir el sistema de Line of Sight

El proyecto ya tiene un script de **LineOfSight**, pero contiene un error.

Deben lograr que la detección del jugador funcione correctamente teniendo en cuenta:

- **Distancia**
- **Ángulo**
- **Obstáculos**

El enemigo **no debería detectar al jugador si una pared bloquea la visión**.

---

## Enemy 1: FSM

El primer enemigo trabaja con una **Máquina de Estados Finitos**.

### Comportamiento esperado

Este enemigo debe tener un comportamiento **miedoso**:

- Si **no ve al jugador**, debe **patrullar**
- Si **ve al jugador** y este **no está demasiado cerca**, debe **perseguirlo**
- Si el jugador **está demasiado cerca**, debe **huir**

### Lo que tienen que revisar

- Los **estados**
- Las **transiciones**
- La integración entre la FSM y los **Steering Behaviours**
- La ejecución correcta del movimiento según el estado actual

### Resultado esperado

El enemigo debe:

- patrullar usando los **patrol points**
- perseguir usando el steering correspondiente
- huir usando el steering correspondiente

---

## Enemy 2: Decision Tree

El segundo enemigo trabaja con un **Árbol de Decisión**.

### Comportamiento esperado

- Si **no ve al jugador**, debe quedarse en un comportamiento simple de **patrulla/rotación**
- Si **ve al jugador** y está **lejos**, debe **perseguirlo**
- Si **ve al jugador** y está **cerca**, debe **huir**

### Lo que tienen que revisar

- La estructura del árbol
- Los **QuestionNode**
- Los **ActionNode**
- La integración entre el árbol y los **Steering Behaviours**

### Resultado esperado

El árbol debe decidir correctamente entre:

- Patrol
- Pursue
- Flee

---

## Steering Behaviours

El proyecto ya incluye un archivo de **SteeringBehaviours**.

La idea no es reescribir todo desde cero, sino **usar correctamente los behaviours ya disponibles** para que los enemigos se muevan según su lógica.

---

## Qué se evalúa

Se tendrá en cuenta:

- Que el **Line of Sight** esté corregido
- Que el **Enemy 1** con FSM funcione correctamente
- Que el **Enemy 2** con Decision Tree funcione correctamente
- Que ambos enemigos usen correctamente los **Steering Behaviours**
- Que la integración entre **percepción**, **decisión** y **movimiento** sea coherente

---

## Aclaraciones

- El **Player** ya está preparado y controlable
- Los **Steering Behaviours** ya están presentes en el proyecto
- La escena base ya tiene enemigos, jugador y obstáculos
- No hace falta agregar sistemas nuevos fuera de lo visto en la primera entrega

---

## Resumen rápido

### Enemy 1
- Patrulla si no ve al jugador
- Persigue si lo ve y está lejos
- Huye si lo ve y está cerca

### Enemy 2
- Patrulla o rota si no ve al jugador
- Persigue si lo ve y está lejos
- Huye si lo ve y está cerca

### General
- Corregir Line of Sight
- Integrar percepción, decisión y movimiento
