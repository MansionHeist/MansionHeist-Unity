# Mansion Heist

[Join Mansion Heist Now!](http://158.247.249.12/)

> 저택의 보물을 노리고 침입한 도둑을 모두 잡아라!  
> 저택의 삼엄한 보안을 전부 뚫고 보물을 훔쳐라!

- 경찰과 도둑을 모티브로 한 술래잡기 게임입니다.
- 경찰은 모든 도둑을 잡아야 승리합니다.
- 도둑은 저택의 보안을 해제하는 미션들을 완료해야 승리합니다.

| <img width="300" alt="KakaoTalk_20230727_171252964" src="https://github.com/MansionHeist/.github/assets/138105180/fc6b476a-0c95-44e5-8359-4a9f8c0f72dc"> | <img width="300" alt="KakaoTalk_20230727_171551894" src="https://github.com/MansionHeist/.github/assets/138105180/4d7de65a-b07d-4ff5-be6c-b2a5cc9a9372"> | <img width="300" alt="F2_revealed (1)" src="https://github.com/MansionHeist/.github/assets/138105180/2183edfe-b508-493e-a8de-e96ed3dbe50c"> |
| :------------------------------------------------------------------------------------------------------------------------------------------------------: | :------------------------------------------------------------------------------------------------------------------------------------------------------: | :-----------------------------------------------------------------------------------------------------------------------------------------: |
|                                                                       Main Screen                                                                        |                                                                       Play Screen                                                                        |                                                                  Game Map                                                                   |

## A. 개발 팀원

- 안지민 <a href="https://github.com/retro3014" target="_blank"><img src="https://img.shields.io/badge/GitHub-181717?style=flat&logo=github&logoColor=white"/></a>
- 이혜민 <a href="https://github.com/coitloz88" target="_blank"><img src="https://img.shields.io/badge/GitHub-181717?style=flat&logo=github&logoColor=white"/></a>
- 김하진 <a href="https://github.com/gkwls1012" target="_blank"><img src="https://img.shields.io/badge/GitHub-181717?style=flat&logo=github&logoColor=white"/></a>

## B. 개발 환경

### Client

- OS: Windows 11
- Target: Web(Chrome, Edge, Firefox)
- Development Tool: Unity 2022.3.5f1 LTS

### Server

- Node.js
- Express
- Socket.io

### Web

- Nginx

## C. 게임 소개

### 1) Main Scene

- 닉네임을 입력하고 방에 입장합니다.

### 2) Room Scene

- 입장할 방을 선택할 수 있습니다.
- 새롭게 방을 생성할 수 있습니다.

### 3) Game Scene

#### 게임 인트로

랜덤으로 배정된 자신의 역할이 Intro로 표시됩니다.

#### 도둑이 해결해야할 11개의 서로 다른 미션들

- 도둑들은 맵을 돌아다니며 미션을 수행합니다.
- 미션 진행률을 상단의 progress bar로 확인할 수 있습니다.

|     ![PWLock](https://github.com/MansionHeist/MansionHeist-Unity/assets/138105180/c9b72b93-76d9-455b-b247-7bc3dfdfeca5)     | ![PeriodicPW](https://github.com/MansionHeist/MansionHeist-Unity/assets/138105180/a977233b-10ec-4798-9927-11768a32a83f)  |
| :-------------------------------------------------------------------------------------------------------------------------: | :----------------------------------------------------------------------------------------------------------------------: |
|                                                     UnLock closet room                                                      |                                                  Periodic table puzzle                                                   |
|  ![NineButtons](https://github.com/MansionHeist/MansionHeist-Unity/assets/138105180/02396551-bedd-46a6-a16f-bf285ebbe6a9)   | ![MessyButton](https://github.com/MansionHeist/MansionHeist-Unity/assets/138105180/1c18dbf0-76ef-4a75-b1fe-709c648d4bb1) |
|                                                     Press nine buttons                                                      |                                                 Find a button below box                                                  |
|   ![FiveSwitch](https://github.com/MansionHeist/MansionHeist-Unity/assets/138105180/5367d34c-db2f-4db6-a7c8-16db69b71035)   |  ![DoNotMove](https://github.com/MansionHeist/MansionHeist-Unity/assets/138105180/e4e1a90f-84e6-4e3f-93fd-0efdf5eabf62)  |
|                                                      Turn off switches                                                      |                                             Do not push button for 4 seconds                                             |
| ![DetroyedButton](https://github.com/MansionHeist/MansionHeist-Unity/assets/138105180/2348a50a-f895-4fc4-b8b1-c1b4e872819c) |  ![DeskLock](https://github.com/MansionHeist/MansionHeist-Unity/assets/138105180/b0c7d4a2-3241-4c00-b2a7-741de287be26)   |
|                                              Find a button inside wooden wall                                               |                                                   Unlock owner's study                                                   |
|   ![BookShelf](https://github.com/MansionHeist/MansionHeist-Unity/assets/138105180/d1af4b21-9420-42fa-8806-211bf6f2dfcc)    | ![Animaleyes](https://github.com/MansionHeist/MansionHeist-Unity/assets/138105180/3c02871a-b2ee-40c2-81cb-4618c8126d8e)  |
|                                                    Select books in order                                                    |                                                   Press eyes of a lion                                                   |
|   ![RedButton](https://github.com/MansionHeist/MansionHeist-Unity/assets/138105180/d02baa84-819d-4fa3-a4f0-43bfd3be84b6)    |                                                            -                                                             |
|                                                    Press the red button                                                     |                                                            -                                                             |

#### 알람

- 미션을 수행하다가 잘못된 입력을 넣는 경우 보안 알람으로 도둑의 위치가 노출됩니다.

<div align="center">
    <img alt="warning alert" src="https://github.com/MansionHeist/MansionHeist-Unity/assets/138105180/76314b7f-4561-4178-9327-d15c3fba1d27" width="400">
</div>

#### 체포

- 경찰은 체포 버튼으로 일정 반경 내의 도둑을 체포할 수 있습니다.
- 체포된 도둑은 철장에 갇혀 움직이지 못합니다.
- 체포되지 않은 도둑이 체포된 도둑을 풀어줄 수 있습니다.

<div align="center">
    <img alt="체포된 모습" src="https://github.com/MansionHeist/.github/assets/88723775/5cad702c-589a-4a73-9d7f-2090695d84b9" width="390">
</div>

#### 게임 종료

| ![image](https://github.com/MansionHeist/.github/assets/88723775/de7d367b-a502-48ef-a1a2-63936dce8c07) | ![image](https://github.com/MansionHeist/.github/assets/88723775/af7209b6-c03a-42bb-8070-087dcf4c5f40) |
| :----------------------------------------------------------------------------------------------------: | :----------------------------------------------------------------------------------------------------: |
|                                               승리 화면                                                |                                               패배 화면                                                |
