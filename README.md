# Wastrium

모션캡처와 대형 스크린을 활용한 분리수거 XR 게임입니다. 해양오염에 대한 피드백을 게임으로 전달하며, 이름은 Waste와 Aquarium을 결합한 것입니다.

## 프로젝트 배경

한국컴퓨터그래픽스학회 KCGS 2023 관련 연구 프로젝트입니다. 이영성은 게임의 주요 개발과 프로젝트 관리를 맡았으며, 지도교수와 함께 OptiTrack–Motive의 추적 데이터를 이용한 제스처 인식 C# 스크립트를 작성했습니다.

## 팀

CGLab(컴퓨터그래픽스 연구실) — 이영성, 김윤섭, 김현진, 유승은, 채지훈

## 개발 환경

| 항목 | 값 |
| --- | --- |
| Unity Editor | **2018.2.1f1** |
| Unity 프로젝트 폴더 | `CASA/` |
| 기본 브랜치 | `main` |

Unity 버전은 [ProjectVersion.txt](CASA/ProjectSettings/ProjectVersion.txt)에 기록된 값을 기준으로 합니다.

- OptiTrack 장비 및 Motive
- Immersion Pro
- 대형 스크린과 모션캡처를 사용할 수 있는 공간

## 시작하기

```bash
git clone --branch main https://github.com/ReYoungSung/Wastrium.git
```

1. Unity Hub에 위 버전의 Editor를 설치합니다.
2. 저장소 안의 `CASA` 폴더를 프로젝트로 추가합니다. 저장소 최상위 폴더와 Unity 프로젝트 폴더는 서로 다릅니다.
3. 최초 임포트와 Package Manager의 패키지 복원을 완료합니다.
4. `Assets/Scenes/InGame.unity`을 열고 Play를 실행합니다.

현재 빌드 설정의 첫 씬은 `InGame.unity`, 두 번째는 `RealFinalStartScene.unity`입니다. 이름만 보고 시작 순서를 바꾸기 전에 각 씬의 초기화와 전환 동작을 확인합니다. OptiTrack 데이터 수신 및 전시 화면 설정을 완료해야 현장 상호작용을 재현할 수 있습니다.

빌드 구성은 [EditorBuildSettings.asset](CASA/ProjectSettings/EditorBuildSettings.asset), 패키지 목록은 [manifest.json](CASA/Packages/manifest.json)에서 확인할 수 있습니다. 아래 `Assets/` 경로는 Unity 프로젝트 폴더 기준입니다.

## 주요 폴더

| 경로 | 내용 |
| --- | --- |
| [Assets/Scenes/](CASA/Assets/Scenes/) | 게임·시작·미션 씬 |
| [Assets/Scripts/](CASA/Assets/Scripts/) | 쓰레기 생성·점수·손동작 인식 로직 |
| [Assets/OptiTrack/](CASA/Assets/OptiTrack/) | 모션캡처 연동 |

## 개발 참고

- `HandTracker.cs`에서 수평·수직 이동과 원형 제스처를 판별합니다.
- 저장소 루트의 `CASA.exe`와 Unity 소스 폴더 `CASA/`를 구분합니다. 개발할 때는 `CASA/`를 Unity 프로젝트로 엽니다.
- Unity 2018.2.1f1 기반 프로젝트이므로 기존 버전에서 먼저 확인한 뒤 엔진 업그레이드를 검토합니다.
