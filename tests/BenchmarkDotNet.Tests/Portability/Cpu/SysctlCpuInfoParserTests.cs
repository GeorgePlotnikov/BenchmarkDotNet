﻿using BenchmarkDotNet.Portability.Cpu;
using Xunit;

namespace BenchmarkDotNet.Tests.Portability.Cpu
{
    public class SysctlCpuInfoParserTests
    {
        [Fact]
        public void EmptyTest()
        {
            var parser = SysctlCpuInfoParser.ParseOutput(string.Empty);
            Assert.Equal(null, parser.ProcessorName);
            Assert.Equal(null, parser.PhysicalProcessorCount);
            Assert.Equal(null, parser.PhysicalCoreCount);
            Assert.Equal(null, parser.LogicalCoreCount);
        }

        [Fact]
        public void MalformedTest()
        {
            var parser = SysctlCpuInfoParser.ParseOutput("malformedkey=malformedvalue\n\nmalformedkey2=malformedvalue2");
            Assert.Equal(null, parser.ProcessorName);
            Assert.Equal(null, parser.PhysicalProcessorCount);
            Assert.Equal(null, parser.PhysicalCoreCount);
            Assert.Equal(null, parser.LogicalCoreCount);
        }

        [Fact]
        public void RealOneProcessorFourCoresTest()
        {
            #region cpuInfo

            var cpuInfo = @"hw.ncpu: 8
hw.byteorder: 1234
hw.memsize: 17179869184
hw.activecpu: 8
hw.physicalcpu: 4
hw.physicalcpu_max: 4
hw.logicalcpu: 8
hw.logicalcpu_max: 8
hw.cputype: 7
hw.cpusubtype: 8
hw.cpu64bit_capable: 1
hw.cpufamily: 280134364
hw.cacheconfig: 8 2 2 8 0 0 0 0 0 0
hw.cachesize: 17179869184 32768 262144 6291456 0 0 0 0 0 0
hw.pagesize: 4096
hw.pagesize32: 4096
hw.busfrequency: 100000000
hw.busfrequency_min: 100000000
hw.busfrequency_max: 100000000
hw.cpufrequency: 2200000000
hw.cpufrequency_min: 2200000000
hw.cpufrequency_max: 2200000000
hw.cachelinesize: 64
hw.l1icachesize: 32768
hw.l1dcachesize: 32768
hw.l2cachesize: 262144
hw.l3cachesize: 6291456
hw.tbfrequency: 1000000000
hw.packages: 1
hw.optional.floatingpoint: 1
hw.optional.mmx: 1
hw.optional.sse: 1
hw.optional.sse2: 1
hw.optional.sse3: 1
hw.optional.supplementalsse3: 1
hw.optional.sse4_1: 1
hw.optional.sse4_2: 1
hw.optional.x86_64: 1
hw.optional.aes: 1
hw.optional.avx1_0: 1
hw.optional.rdrand: 1
hw.optional.f16c: 1
hw.optional.enfstrg: 1
hw.optional.fma: 1
hw.optional.avx2_0: 1
hw.optional.bmi1: 1
hw.optional.bmi2: 1
hw.optional.rtm: 0
hw.optional.hle: 0
hw.optional.adx: 1
hw.optional.mpx: 0
hw.optional.sgx: 0
hw.optional.avx512f: 0
hw.optional.avx512cd: 0
hw.optional.avx512dq: 0
hw.optional.avx512bw: 0
hw.optional.avx512vl: 0
hw.optional.avx512ifma: 0
hw.optional.avx512vbmi: 0
hw.targettype: Mac
hw.cputhreadtype: 1
machdep.cpu.max_basic: 13
machdep.cpu.max_ext: 2147483656
machdep.cpu.vendor: GenuineIntel
machdep.cpu.brand_string: Intel(R) Core(TM) i7-4770HQ CPU @ 2.20GHz
machdep.cpu.family: 6
machdep.cpu.model: 70
machdep.cpu.extmodel: 4
machdep.cpu.extfamily: 0
machdep.cpu.stepping: 1
machdep.cpu.feature_bits: 9221959987971750911
machdep.cpu.leaf7_feature_bits: 10155
machdep.cpu.extfeature_bits: 142473169152
machdep.cpu.signature: 263777
machdep.cpu.brand: 0
machdep.cpu.features: FPU VME DE PSE TSC MSR PAE MCE CX8 APIC SEP MTRR PGE MCA CMOV PAT PSE36 CLFSH DS ACPI MMX FXSR SSE SSE2 SS HTT TM PBE SSE3 PCLMULQDQ DTES64 MON DSCPL VMX EST TM2 SSSE3 FMA CX16 TPR PDCM SSE4.1 SSE4.2 x2APIC MOVBE POPCNT AES PCID XSAVE OSXSAVE SEGLIM64 TSCTMR AVX1.0 RDRAND F16C
machdep.cpu.leaf7_features: SMEP ERMS RDWRFSGS TSC_THREAD_OFFSET BMI1 AVX2 BMI2 INVPCID FPU_CSDS
machdep.cpu.extfeatures: SYSCALL XD 1GBPAGE EM64T LAHF LZCNT RDTSCP TSCI
machdep.cpu.logical_per_package: 16
machdep.cpu.cores_per_package: 8
machdep.cpu.microcode_version: 19
machdep.cpu.processor_flag: 5
machdep.cpu.mwait.linesize_min: 64
machdep.cpu.mwait.linesize_max: 64
machdep.cpu.mwait.extensions: 3
machdep.cpu.mwait.sub_Cstates: 270624
machdep.cpu.thermal.sensor: 1
machdep.cpu.thermal.dynamic_acceleration: 1
machdep.cpu.thermal.invariant_APIC_timer: 1
machdep.cpu.thermal.thresholds: 2
machdep.cpu.thermal.ACNT_MCNT: 1
machdep.cpu.thermal.core_power_limits: 1
machdep.cpu.thermal.fine_grain_clock_mod: 1
machdep.cpu.thermal.package_thermal_intr: 1
machdep.cpu.thermal.hardware_feedback: 0
machdep.cpu.thermal.energy_policy: 1
machdep.cpu.xsave.extended_state: 7 832 832 0
machdep.cpu.xsave.extended_state1: 1 0 0 0
machdep.cpu.arch_perf.version: 3
machdep.cpu.arch_perf.number: 4
machdep.cpu.arch_perf.width: 48
machdep.cpu.arch_perf.events_number: 7
machdep.cpu.arch_perf.events: 0
machdep.cpu.arch_perf.fixed_number: 3
machdep.cpu.arch_perf.fixed_width: 48
machdep.cpu.cache.linesize: 64
machdep.cpu.cache.L2_associativity: 8
machdep.cpu.cache.size: 256
machdep.cpu.tlb.inst.large: 8
machdep.cpu.tlb.data.small: 64
machdep.cpu.tlb.data.small_level1: 64
machdep.cpu.tlb.shared: 1024
machdep.cpu.address_bits.physical: 39
machdep.cpu.address_bits.virtual: 48
machdep.cpu.core_count: 4
machdep.cpu.thread_count: 8
machdep.cpu.tsc_ccc.numerator: 0
machdep.cpu.tsc_ccc.denominator: 0";

            #endregion

            var parser = SysctlCpuInfoParser.ParseOutput(cpuInfo);
            Assert.Equal("Intel(R) Core(TM) i7-4770HQ CPU @ 2.20GHz", parser.ProcessorName);
            Assert.Equal(1, parser.PhysicalProcessorCount);
            Assert.Equal(4, parser.PhysicalCoreCount);
            Assert.Equal(8, parser.LogicalCoreCount);
        }
    }
}