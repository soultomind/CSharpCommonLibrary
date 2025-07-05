# CSharpCommonLibrary 프로젝트 설명

## 개요
**CSharpCommonLibrary**는 .NET Framework 4.5를 대상으로 하는 범용 C# 유틸리티 라이브러리입니다.  
다양한 환경, 문자열, 이미지, 리플렉션, UI, 확장 메서드 등 여러 범용 기능을 제공합니다.

## 주요 폴더 및 파일 구조
- **Utilities**: 환경, 문자열, 이미지, 캘린더, 정규식, 레지스트리 등 유틸리티 클래스
- **Reflection**: 어셈블리 빌드 정보 및 속성 관련 클래스
- **UI**: 사용자 인터페이스 관련 컨트롤 및 다이얼로그
- **Extensions**: Enum, Attribute, Screen, RichTextBox 등 확장 메서드
- **IO**: 파일 및 이미지 생성 정보 관리
- **Net35**: .NET 3.5 전용 확장 클래스
- **기타**: 인증서, 로그 레벨, 화면 관리 등

---

# Public API 요약

## 주요 클래스 및 기능

### Utilities
- **EnvironmentUtility**: 환경 변수 및 시스템 정보 관련 유틸리티
- **ColorUtility**: 색상 변환 및 처리
- **ImageUtility**: 이미지 변환 및 처리
- **StringUtility**: 문자열 처리 유틸리티
- **CalendarUtility**: 달력 및 날짜 관련 기능
- **RegexUtility**: 정규식 관련 유틸리티
- **RegistryUtility**: 레지스트리 접근 및 조작
- **MathUtility**: 수학 관련 유틸리티
- **ConverUtility**: 데이터 변환 유틸리티

### Reflection
- **AssemblyBuildInfo**: 어셈블리 빌드 정보 제공
- **AssemblyBuildDateAttribute**: 빌드 날짜 속성

### UI
- **TablessControl**: 탭 없는 컨트롤
- **ScreenIndexDialog**: 화면 인덱스 선택 다이얼로그
- **ScreenIndexDialogColor**: 색상 지정 다이얼로그

### Extensions
- **EnumExtension**: Enum 관련 확장 메서드
- **AttributeExtension**: Attribute 관련 확장 메서드
- **ScreenExtension**: 화면 관련 확장 메서드
- **RichTextBoxExtension**: RichTextBox 관련 확장 메서드

### IO
- **ImageFileCreateInfo**: 이미지 파일 생성 정보
- **FileCreateInfo**: 파일 생성 정보
- **ImageManager**: 이미지 파일 관리

### 기타
- **CertFile**: 인증서 파일 관리
- **LogLevel**: 로그 레벨 정의
- **AspectRatio, AspectRatioF**: 화면 비율 구조체
- **ShowScreenIndex, ScreenManager**: 화면 인덱스 및 관리
- **ExternalLoggingEventArgs**: 외부 로깅 이벤트 인자
- **FirefoxPreference, EdgeWebView2Runtime**: 브라우저 관련 설정

---
