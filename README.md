### CSharpCommonLibrary 소개
CSharpCommonLibrary는 .NET Framework 4.5 기반의 다양한 공통 유틸리티, 확장 메서드, 시스템/환경 도우미, 파일/이미지/스크린 관리, 로깅, 리플렉션, Enum/Attribute/문자열/수학 유틸리티 등을 제공하는 라이브러리입니다.

- 주요 클래스 및 기능 예시:
  - `AspectRatio`, `AspectRatioF`: 화면/이미지 비율 계산
  - `CertFile`: 인증서 파일 관리
  - `ColorData`: Color/ARGB/Hex 변환 및 색상 정보 래핑
  - `LogLevel`: 로깅 레벨 관리
  - `Toolkit`: 디버깅/트레이스 및 관리자 권한 체크 등
  - `EnvironmentUtility`, `MathUtility`, `StringUtility`: 환경, 수학, 문자열 관련 유틸리티
  - `ScreenManager`: 멀티 모니터 환경 지원
  - 다양한 확장 메서드 및 속성(Attribute) 관련 도우미

### 주요 클래스/열거형 설명

- **AspectRatio**: 정수형 화면/이미지 비율을 계산하고 표현하는 클래스
- **AspectRatioF**: 실수형 화면/이미지 비율을 계산하고 표현하는 클래스
- **CertFile**: 인증서 파일을 시스템에 추가/관리하는 기능 제공
- **ColorData**: Color, int ARGB, Hex 문자열 간 변환 및 색상 정보(알파, ARGB, Hex 등) 제공
- **CertFileStoreStatus (enum)**: 인증서 파일 저장 상태를 나타내는 열거형
- **Compare (enum)**: 비교 연산(크다, 작다, 같다)을 나타내는 열거형
- **Explorer**: Windows explorer 프로세스 관련 상수 제공
- **ExternalLoggingEventArgs**: 외부 로깅 이벤트 정보를 담는 클래스
- **FileCreateInfo**: 파일 생성 시 옵션(잠금, 모드, 접근권한) 정보를 담는 클래스
- **LogLevel**: 다양한 로깅 레벨(ALL, TRACE, DEBUG, INFO, WARN, ERROR, FATAL, OFF) 관리
- **ScreenManager**: 멀티 모니터 환경에서 스크린 정보 및 인덱스 관리
- **StringResourceAttribute**: 문자열 리소스(포맷/메시지)용 커스텀 Attribute
- **EnvironmentUtility**: 시스템 환경, 폴더 경로 등 환경 관련 유틸리티 제공
- **MathUtility**: 수학적 계산(최대/최소/최대공약수/좌표변환 등) 유틸리티 제공
- **StringUtility**: 문자열 관련 유틸리티(대소문자 변환, 알파벳 판별 등) 제공

### Coding Convention   
해당 저장소의 코딩 표준 가이드는 아래 링크의 포프님 C# 코딩 표준 을 따릅니다.   
https://docs.popekim.com/ko/coding-standards/csharp
