# Shape Defense 🛡️

> 도형을 활용한 전략 디펜스 게임, Unity 2D 제작

## ✨ Features

- 도형 유닛을 소환하고 강화하여 적 처치
- 자동 전투 및 물리 기반 탄환 시스템
- 코인 수집 → 업그레이드 루프
- 반복 가능한 웨이브 구조
- 유닛 합성(Combine) 시스템으로 깊이 있는 성장 루프
- 8개 맵 / 8가지 적 이동 패턴
- 업적 / 일일·주간 퀘스트 / 출석 체크 / 상점 시스템

## 🛠 Tech Stack

- **Engine**: Unity 6000.0.63f1
- **Render Pipeline**: Universal Render Pipeline (URP) 17.0.4
- **Language**: C#
- **Database**: SQLite (Mono.Data.Sqlite)
- **Ads**: Google Mobile Ads SDK
- **Target Platform**: Android (640x960 세로 모드)

## 📱 다운로드

- [Google Play](https://play.google.com/store/apps/details?id=com.SunhosWorld.ShapeDefense)

---

## 🎮 게임 개요

플레이어는 다양한 도형(원, 사각형, 삼각형 등) 유닛을 필드에 배치하고, 같은 유닛을 합성(Combine)하여 더 강한 유닛으로 진화시키면서 8개의 맵에서 몰려오는 적들을 막아내는 타워 디펜스 게임입니다.

### 주요 시스템

- **유닛** — 도형/타입/등급별 유닛, 강화(Upgrade), 합성(Combine), 판매(Sell), 리롤(Reroll)
- **전투** — 자동 공격, 공격 범위(AttackArea), 총알 풀링, 보스 처치 보상
- **8개 맵** — `MapScene1` ~ `MapScene8`, 각각 다른 적 이동 패턴
- **콘텐츠**
  - 업적(Achievement)
  - 일일 퀘스트(Daily Quest) / 주간 퀘스트(Weekly Quest)
  - 출석 체크(Attendance Check)
  - 상점(Shop) / 상자 구매(Chest) / 스태미나 구매
- **편의 기능** — 게임 속도 조절, 일시정지, 닉네임 설정, 색상 커스터마이징, 난이도 선택
- **수익화** — Google Mobile Ads (`GoogleAdvManager`)

---

## 📁 프로젝트 구조

```
ShapeDefense_2024.6/
├── Assets/
│   ├── Animate/                    # 애니메이션 에셋
│   ├── Font/                       # 폰트 (DNFBitBitv2 SDF 등)
│   ├── Plugins/                    # Mono.Data.Sqlite, Google Ads 등 외부 플러그인
│   ├── Resources/
│   │   ├── Prefabs/                # Bullet, Enemy, PieceUnit, Quest 등 런타임 로드 프리팹
│   │   └── Sprite/                 # Background, Bullet, Enemy, Unit, Icon, Stage, Tile (아틀라스 포함)
│   ├── Scenes/
│   │   ├── GameStartScene.unity    # 타이틀
│   │   ├── LoadScene.unity         # 로딩
│   │   ├── SettingScene.unity      # 설정
│   │   ├── ShopScene.unity         # 상점
│   │   └── Field/                  # MapScene1 ~ MapScene8 (인게임 필드)
│   ├── Script/
│   │   ├── Adv/                    # Google Mobile Ads
│   │   ├── Bullet/                 # BulletMove, BulletPool
│   │   ├── DB/                     # SQLite 연결, 업적/퀘스트 컨트롤러, 권한 매니저
│   │   │   └── Connector/          # 업적/일일/주간 퀘스트 커넥터
│   │   ├── DataClass/              # 핵심 데이터 클래스 (Unit, Enemy, User, DataHub, Singleton 등)
│   │   ├── Enemy/                  # 적 스폰, 풀링, HP, 라운드 진행
│   │   │   └── Move/               # 8가지 적 이동 패턴
│   │   ├── Function/               # 출석체크, 구매 확인, 게임 속도, 강화 등 게임 기능
│   │   ├── Interface/              # IStatement, QuestConnector 인터페이스
│   │   ├── Observer/               # Core/Damage/Quest 등 옵저버 패턴
│   │   ├── SceneManager/           # 씬 전환, 인게임 데이터 허브 연결
│   │   ├── UIControll/             # 모든 UI 제어 (메뉴, 패널, 상점 등)
│   │   └── Unit/                   # 유닛 생성, 공격, 합성, 판매, 리롤
│   ├── Settings/                   # URP 설정
│   ├── Shader/                     # 커스텀 셰이더
│   ├── StreamingAssets/
│   │   └── ShapeDefenseDB.db       # SQLite 게임 데이터베이스
│   └── TextMesh Pro/               # TMP 에셋
├── Packages/                       # Unity 패키지 매니페스트
└── ProjectSettings/                # Unity 프로젝트 설정
```

> Android 서명 키(`*.keystore`)는 보안상 저장소에 포함되지 않습니다. 빌드 시 별도로 관리해야 합니다.

---

## 🔧 핵심 시스템

### 데이터 허브 (GameData)

`Assets/Script/DataClass/ShapeDefense.cs`에 위치한 `GameData` 클래스가 게임 전역에서 사용하는 싱글톤 인스턴스와 프리팹 참조, 공통 `WaitForSeconds` 객체 등을 정적으로 보관합니다. 각 유닛의 공격 속도에 따라 미리 캐시된 `WaitForSeconds`를 재활용하여 GC 부담을 줄입니다.

### 데이터베이스

`Mono.Data.Sqlite`를 사용하여 `StreamingAssets/ShapeDefenseDB.db`에 접근합니다. 유닛 ID는 4자리 코드 체계(예: 1001~, 2001~, 3001~)로 카테고리화되어 있습니다.

### 옵저버 패턴

`Assets/Script/Observer/` 하위에 코어/데미지/퀘스트 수신/유닛 업그레이드 등 다수의 옵저버가 구현되어 있어, UI와 게임 로직 간 결합도를 낮춥니다.

---

## 🚀 빌드 및 실행

1. Unity Hub에서 **6000.0.63f1** 버전으로 프로젝트를 엽니다.
2. `Assets/Scenes/GameStartScene.unity`에서 게임 시작이 가능합니다.
3. Android 빌드 시 별도로 보관된 서명 키스토어를 `Project Settings > Player > Publishing Settings`에 지정합니다 (키 파일 및 비밀번호는 저장소에 포함되지 않음).

---

## 📦 주요 의존성

- `com.unity.feature.2d` 2.0.1
- `com.unity.render-pipelines.universal` 17.0.4
- `com.unity.ugui` 2.0.0
- `com.unity.timeline` 1.8.9
- `Mono.Data.Sqlite` (플러그인)

---

## 📄 라이선스

본 프로젝트는 **SunhosWorld**의 자산입니다.
